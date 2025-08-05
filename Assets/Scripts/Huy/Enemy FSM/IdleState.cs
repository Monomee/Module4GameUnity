using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{ 
    private Enemy enemyController;
    private const string IDLE_ANIMATION_BOOL = "isIdle";


    public IdleState(Enemy enemyController)
    {
        this.enemyController = enemyController;
    }

    public void Enter()
    {
        enemyController.animator.SetBool(IDLE_ANIMATION_BOOL, true);
    }

    public void Update()
    {
        if(enemyController.DetectedPlayer())
        {
            enemyController.enemyStateMachine.ChangeState(new SwordDrawState(enemyController));
        }

    }

    public void Exit()
    { 
    enemyController.animator.SetBool(IDLE_ANIMATION_BOOL, false);
    }
}
