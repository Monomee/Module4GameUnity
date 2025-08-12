using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitEvent : MonoBehaviour
{
    public bool isMelee;

    private Enemy enemy;
    private MageEnemy mage;
    private KnightEnemy knight;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        mage = GetComponent<MageEnemy>();
        knight = GetComponent<KnightEnemy>();
    }
    
    public void DoAttack()
    {
        // FSM is NOT allowed to attack
        if (!enemy.isReadyToAttack) return;

        if (isMelee)
        {
            MeleeAttack();
        }

        else
        {
            ShootAttack();
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

    private void ShootAttack()
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
}
