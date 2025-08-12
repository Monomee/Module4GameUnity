using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageIdleState : IEnemyState
{
    private Enemy enemyController;
    private const string MAGE_IDLE_ANIMATION_BOOL = "isIdle";

    public MageIdleState(Enemy enemyController)
    {
        this.enemyController = enemyController;
    }
    public void Enter()
    {
        enemyController.animator.SetBool(MAGE_IDLE_ANIMATION_BOOL, true);
    }


    public void Update()
    {
        if (enemyController.DetectedPlayer())
        {
            enemyController.enemyStateMachine.ChangeState(new MageWalkState(enemyController));
        }
    }

    public void Exit()
    {
        enemyController.animator.SetBool(MAGE_IDLE_ANIMATION_BOOL, false);
    }
}
