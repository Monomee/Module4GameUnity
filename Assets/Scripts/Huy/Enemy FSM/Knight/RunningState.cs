using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IEnemyState
{
    private Enemy enemyController;
    private const string RUNNING_ANIMATION_TRIGGER = "isRunning";
    // Start is called before the first frame update

    public RunningState(Enemy enemyController)
    { 
        this.enemyController = enemyController;
    }
    public void Enter()
    {
        enemyController.animator.SetBool(RUNNING_ANIMATION_TRIGGER, true);

    }


    public void Update()
    {
        enemyController.FaceTarget();
        enemyController.agent.SetDestination(enemyController.player.position);
        // Attack
        AttackPlayer();
    }

    public void Exit() 
    {
        enemyController.animator.SetBool(RUNNING_ANIMATION_TRIGGER, false);
    }
    private void AttackPlayer()
    {
        float distance = Vector3.Distance(enemyController.transform.position, enemyController.player.position);
        if (distance <= enemyController.attackRange)
        {
            enemyController.enemyStateMachine.ChangeState(new AttackState(enemyController));
        }
    }
    
}
