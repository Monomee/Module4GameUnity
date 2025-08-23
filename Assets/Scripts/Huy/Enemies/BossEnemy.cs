using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : Enemy
{

    [Header("Summon Adds")]
    public TestPool spawnEnemies;
    public int addsCount = 3;             // số lính gọi ra mỗi lần
    public float summonCD = 10f;          // hồi chiêu
    public float summonRadius = 4f;       // bán kính spawn quanh tâm
    public bool spawnAroundPlayer = true; // true = gọi quanh Player, false = quanh Boss
    public float telegraphSummonTime = 0.8f;

    [Header("Sword Rain")]
    public TestPool swordPool;              // kiếm có Rigidbody
    public float swordRainCD = 3f;
    public float rainRadius = 3.5f;             // bán kính vòng mưa kiếm
    public int swordsCount = 12;                 // số kiếm rơi
    public float spawnHeight = 12f;
    public float fallSpeed = 25f;
    public bool lockOnPlayer = true;            // true = rơi ngay chỗ player đang đứng
    public Transform arenaCenter;
    public float arenaRadius = 20f;
    public float telegraphSwordTime = 0.8f;

    public float swordRainMinRange = 8f; //chỉ khi >= 8m

    [Header("Defend")]
    public float defendCD = 5f;
    public float defendDuration = 1.5f;
    public float defendRange = 6f;   // chỉ defend khi < 6m
    public Health defendHealth;
    [Range(0, 1)] public float defendReduce = 0.7f; // 70%

    // runtime
    float nextSummon, nextRain, nextDefend;
    public bool isDefending; //{ get; private set; }
    public bool isCasting;
    Collider[] hitBuf = new Collider[4];

    private void Awake()
    {
        nextRain = Time.time + swordRainCD;
        nextDefend = Time.time + defendCD;
    }

    protected override IEnemyState GetInitialState()
    {
        return new BossIdleState(this);
    }

    public bool CanSummon()
    {
        if (isDefending || isCasting) return false;
        //Chưa gán pool không triệu hồi được
        if (spawnEnemies == null) return false;

        //Chưa hồi không triệu hồi được
        if (Time.time < nextSummon) return false;

        //Có spawner và đã hết hồi 
        return true;
    }

    public bool CanSwordRain()
    {
        //Không được đang phòng thủ
        if (isDefending || isCasting) return false;

        // có pool của chiêu mưa kiếm
        if (swordPool == null) return false;

        // Phải hết hồi chiêu
        if (Time.time < nextRain) return false;

        return true;
    }

    public bool CanDefend()
    {
        //Đang phòng thủ rồi không kích hoạt lại
        if (isDefending || isCasting) return false;

        //Phải hết hồi chiêu
        if (Time.time < nextDefend) return false;

        return true;
    }

    public void StartSwordRain()
    {
        nextRain = Time.time + swordRainCD;
        isCasting = true;
        //agent.ResetPath();
        //agent.isStopped = true;

        if (animator) animator.SetTrigger("swordRain");

        StartCoroutine(SwordRainRoutine());
    }

    IEnumerator SwordRainRoutine()
    {
        yield return new WaitForSeconds(telegraphSwordTime);

        Vector3 center;
        if (lockOnPlayer && player) center = player.position;
        else if (arenaCenter) center = arenaCenter.position;
        else center = transform.position;

        for (int i = 0; i < swordsCount; i++)
        {
            var obj = swordPool.GetPooledObject();
            if (obj == null) continue; // pool hết slot

            Vector2 c = Random.insideUnitCircle * rainRadius;
            Vector3 pos = new Vector3(center.x + c.x, center.y + spawnHeight, center.z + c.y);

            obj.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(Vector3.down));

            // reset rigidbody và "ném" xuống
            var rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.useGravity = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.velocity = Vector3.down * fallSpeed;
            }

            obj.SetActive(true);
        }
        isCasting = false;
    }

    public void StartSummon()
    {
        nextSummon = Time.time + summonCD;
        isCasting = true;
        agent.ResetPath();
        agent.isStopped = true;
        if (animator) animator.SetTrigger("summon");
        StartCoroutine(SummonRoutine());
    }

    IEnumerator SummonRoutine()
    {
        // small telegraph window so players can react
        yield return new WaitForSeconds(telegraphSummonTime);


        Vector3 center = spawnAroundPlayer && player ? player.position : transform.position;
        int spawned = 0;
        for (int i = 0; i < addsCount; i++)
        {
            var obj = spawnEnemies.GetPooledObject();
            if (obj == null) break;
            Vector2 c = Random.insideUnitCircle * summonRadius;
            Vector3 p = new Vector3(center.x + c.x, center.y, center.z + c.y);
            obj.transform.position = p;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
            spawned++;
        }
        // optional: play a VFX/SFX based on 'spawned'

        isCasting = false;
    }

    
    public Coroutine DoDefend()
    {
        return StartCoroutine(DefendRoutine());
    }
     
    IEnumerator DefendRoutine()
    {
        nextDefend = Time.time + defendCD;
        isDefending = true;
        agent.ResetPath();
        agent.isStopped = true;
        animator.SetBool("isDefend", true);
        yield return new WaitForSeconds(defendDuration);
        animator.SetBool("isDefend", false);
        isDefending = false;
        agent.isStopped = false;
    }
   

    public void ApplyDamage(float dmg)
    {
        if (dmg <= 0) return;

        // Đang Defend chỉ nhận 30% sát thương nếu defendReduce = 0.7f
        if (isDefending) 
            dmg *= (1f - defendReduce);

        // Đẩy vào Health
        var hpComp = GetComponent<Health>();
        if (hpComp != null)
        {
            hpComp.OnTakeDmg(dmg);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (arenaCenter)
        {
            Gizmos.DrawWireSphere(arenaCenter.position, arenaRadius);
        }
    }

}
