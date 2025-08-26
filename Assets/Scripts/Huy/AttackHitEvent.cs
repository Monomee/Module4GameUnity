using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitEvent : MonoBehaviour
{
    public bool isKnight;
    public bool isSpider;
    public bool isMage;
    public bool isArcher;

    private Enemy enemy;
    private MageEnemy mage;
    private KnightEnemy knight;
    private ArcherEnemy archer;
    private SpiderEnemy spider;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        mage = GetComponent<MageEnemy>();
        knight = GetComponent<KnightEnemy>();
        archer = GetComponent<ArcherEnemy>();
        spider = GetComponent<SpiderEnemy>();
    }

    public void DoAttack()
    {
        // FSM is NOT allowed to attack
        if (!enemy.isReadyToAttack) return;

        if (isKnight)
        {
            KnightAttack();
        }

        if (isSpider)
        {
            SpiderAttack();
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

    private void KnightAttack()
    {
        if (enemy.player == null) return;

        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (dist <= enemy.attackRange)
        {
            Debug.Log($"{enemy.name} hitted Player, took {knight.knightDamage} damage");

            enemy.player.GetComponent<Health>()?.OnTakeDmg(knight.knightDamage);
        }
    }

    private void SpiderAttack()
    {
        if (enemy.player == null) return;

        float dist = Vector3.Distance(enemy.transform.position, enemy.player.position);
        if (dist <= enemy.attackRange)
        {
            Debug.Log($"{enemy.name} hitted Player, took {spider.spiderDamage} damage");

            enemy.player.GetComponent<Health>()?.OnTakeDmg(spider.spiderDamage);
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
        Vector3 dir = (mage.player.position + Vector3.up * 1.2f) - mage.shootPoint.position;
        Quaternion rot = Quaternion.LookRotation(dir);

        // Hàm này sẽ giúp code ngắn hơn so với 2 dòng dưới
        mageFireBall.transform.SetPositionAndRotation(mage.shootPoint.position, rot); 
        /*
        mageFireBall.transform.position = boss.shootPoint.position;
        mageFireBall.transform.rotation = Quaternion.LookRotation(
            (boss.player.position + Vector3.up * 1.2f) - boss.shootPoint.position
        );
        */
        mageFireBall.SetActive(true);

        var rb = mageFireBall.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = mageFireBall.transform.forward * mage.fireballSpeed;
    }
    /*
        private void ArcherShootAttack()
        {
            if (boss.shootPoint == null || boss.archerArrowPool == null)
            {
                Debug.LogWarning("ShootPoint or archerArrowPool is not attached on ArcherEnemy");
                return;
            }

            var archerArrow = boss.archerArrowPool.GetPooledObject();
            if (archerArrow == null) return;

            archerArrow.transform.position = boss.shootPoint.position;
            archerArrow.transform.rotation = Quaternion.LookRotation(
                (boss.player.position + Vector3.up * 1.2f) - boss.shootPoint.position
            );
            archerArrow.SetActive(true);

            var rb = archerArrow.GetComponent<Rigidbody>();
            if(rb != null)
                rb.velocity = archerArrow.transform.forward * boss.arrowSpeed;
        }
    */

    public void ArcherShootAttack()
    {
        if (archer == null || archer.shootPoint == null || archer.archerArrowPool == null) return;

        var arrow = archer.archerArrowPool.GetPooledObject();
        if (arrow == null) return;

        // Hướng bay tới Player 
        Vector3 dir = (archer.player.position + Vector3.up * 1.2f) - archer.shootPoint.position;
        Quaternion rot = Quaternion.LookRotation(dir);

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


