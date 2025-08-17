using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttackState : IEnemyState
{
    private MageEnemy mage;

    private const string MAGE_ATTACK_ANIMATION_BOOL = "isAttack";

    private float nextShotTime = 0f;

    public MageAttackState(Enemy enemyController)
    {
        mage = (MageEnemy)enemyController;
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
        mage.FaceTarget();

        if (Time.time >= nextShotTime)
        {            
            //Shoot();
            nextShotTime = Time.time + 1f / Mathf.Max(0.01f, mage.fireRate);
        }

    }

    public void Exit()
    {
        mage.animator.SetBool(MAGE_ATTACK_ANIMATION_BOOL, false);
    }

}


