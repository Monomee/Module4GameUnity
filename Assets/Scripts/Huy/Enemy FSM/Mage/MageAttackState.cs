using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttackState : IEnemyState
{
    private MageEnemy mage;

    private Enemy enemyController;

    private const string MAGE_ATTACK_ANIMATION_BOOL = "isAttack";

    private float nextShotTime = 0f;

    public MageAttackState(Enemy enemyController)
    {
        mage = (MageEnemy)enemyController;
        //this.enemyController = enemyController;
    }

    public void Enter()
    {
        mage.animator.SetBool(MAGE_ATTACK_ANIMATION_BOOL, true);
    }

    public void Update()
    {
        if (mage.player == null) return; // ?
        float distance = Vector3.Distance(mage.transform.position, mage.player.position);

        if (distance > mage.attackRange + 0.5f)
        {
            mage.isReadyToAttack = false;
            mage.enemyStateMachine.ChangeState(new MageWalkState(mage));
            return;
        }
        else
        {
            mage.isReadyToAttack = true;
        }

        if (Time.time >= nextShotTime)
        {
            mage.FaceTarget();          
            //Shoot();
            nextShotTime = Time.time + 1f / Mathf.Max(0.01f, mage.fireRate);
        }

    }

    public void Exit()
    {
        mage.animator.SetBool(MAGE_ATTACK_ANIMATION_BOOL, false);
    }
/*
    private void Shoot()
    {
        if(mage.shootPoint == null || mage.mageFireballPool == null)
        {
            Debug.LogWarning("ShootPoint or mageFireballPool is not attached on MageEnemy");
            return;
        }

        var mageFireBall = mage.mageFireballPool.GetPooledObject();

        if (mageFireBall == null) return ;

        mageFireBall.transform.position = mage.shootPoint.position;
        mageFireBall.transform.rotation = Quaternion.LookRotation(
            (mage.player.position + Vector3.up * 1.2f) - mage.shootPoint.position
        );
        mageFireBall.SetActive(true);

        var rb = mageFireBall.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = mageFireBall.transform.forward * mage.fireballSpeed;
    }
*/
}


