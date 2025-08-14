using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageIdleState : IEnemyState
{
    private MageEnemy mage;
    private const string MAGE_IDLE_ANIMATION_BOOL = "isIdle";

    public MageIdleState(Enemy enemyController)
    {
        mage = enemyController as MageEnemy;
    }
    public void Enter()
    {
        mage.animator.SetBool(MAGE_IDLE_ANIMATION_BOOL, true);
    }


    public void Update()
    {
        if (mage.DetectedPlayer())
        {
            mage.enemyStateMachine.ChangeState(new MageWalkState(mage));
        }
    }

    public void Exit()
    {
        mage.animator.SetBool(MAGE_IDLE_ANIMATION_BOOL, false);
    }
}
