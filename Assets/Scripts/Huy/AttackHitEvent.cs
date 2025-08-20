using UnityEngine;

public class AttackHitEvent : MonoBehaviour
{
    public bool isMelee;
    public bool isMage;
    public bool isArcher;

    private Enemy enemy;
    private MageEnemy mage;
    private KnightEnemy knight;
    private ArcherEnemy archer;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        mage = GetComponent<MageEnemy>();
        knight = GetComponent<KnightEnemy>();
        archer = GetComponent<ArcherEnemy>();
    }

    public void DoAttack()
    {
        // FSM is NOT allowed to attack
        if (!enemy.isReadyToAttack) return;

        if (isMelee)
        {
            MeleeAttack();
        }

        if (isMage)
        {
            MageShootAttack();
        }

        if (isArcher)
        {
            ArcherShootAttack();
        }
    }

    private void MeleeAttack()
    {
        if (enemy.player == null) return;

        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (dist <= enemy.attackRange)
        {
            Debug.Log($"{enemy.name} hitted Player, took {knight.knightDamage} damage");

            enemy.player.GetComponent<Health>()?.OnTakeDmg(knight.knightDamage);
        }
    }

    private void MageShootAttack()
    {
        if (mage.shootPoint == null || mage.mageFireballPool == null)
        {
            Debug.LogWarning("ShootPoint or mageFireballPool is not attached on MageEnemy");
            return;
        }

        var mageFireBall = mage.mageFireballPool.GetPooledObject();

        if (mageFireBall == null) return;

        mageFireBall.transform.position = mage.shootPoint.position;
        mageFireBall.transform.rotation = Quaternion.LookRotation(
            (mage.player.position + Vector3.up * 1.2f) - mage.shootPoint.position
        );
        mageFireBall.SetActive(true);

        var rb = mageFireBall.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = mageFireBall.transform.forward * mage.fireballSpeed;
    }
    /*
        private void ArcherShootAttack()
        {
            if (archer.shootPoint == null || archer.archerArrowPool == null)
            {
                Debug.LogWarning("ShootPoint or archerArrowPool is not attached on ArcherEnemy");
                return;
            }

            var archerArrow = archer.archerArrowPool.GetPooledObject();
            if (archerArrow == null) return;

            archerArrow.transform.position = archer.shootPoint.position;
            archerArrow.transform.rotation = Quaternion.LookRotation(
                (archer.player.position + Vector3.up * 1.2f) - archer.shootPoint.position
            );
            archerArrow.SetActive(true);

            var rb = archerArrow.GetComponent<Rigidbody>();
            if(rb != null)
                rb.velocity = archerArrow.transform.forward * archer.arrowSpeed;
        }
    */

    public void ArcherShootAttack()
    {
        if (archer == null || archer.shootPoint == null || archer.archerArrowPool == null) return;
        Debug.Log("archer attack");
        var arrow = archer.archerArrowPool.GetPooledObject();
        if (!arrow) return;

        // Hướng bay tới Player (nâng mục tiêu một chút)
        Vector3 dir = (archer.player.position + Vector3.up * 1.2f) - archer.shootPoint.position;
        Quaternion rot = Quaternion.LookRotation(dir);

        // Đặt world pos/rot TRƯỚC khi bật
        arrow.transform.SetPositionAndRotation(archer.shootPoint.position, rot);
        arrow.SetActive(true);

        // Set vận tốc
        var rb = arrow.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.WakeUp();
            rb.velocity = arrow.transform.forward * archer.arrowSpeed;
        }

        // Truyền thông tin cho mũi tên (để ignore owner + damage)
        var proj = arrow.GetComponent<ArcherArrow>();
        if (proj)
        {
            var ownerCols = archer.GetComponentsInChildren<Collider>();
            proj.Init(archer.transform, ownerCols, proj.arrowDamage);
        }
    }
}


