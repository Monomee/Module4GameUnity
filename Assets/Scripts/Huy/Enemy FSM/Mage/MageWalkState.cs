using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWalkState : IEnemyState
{
    private Enemy enemyController;
    private const string MAGE_WALK_ANIMATION_BOOL = "isWalk";

    public MageWalkState(Enemy enemyController)
    {
        this.enemyController = enemyController;
    }

    public void Enter()
    {
        enemyController.animator.SetBool(MAGE_WALK_ANIMATION_BOOL, true);
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
        enemyController.animator.SetBool(MAGE_WALK_ANIMATION_BOOL, false);
    }

    private void AttackPlayer()
    {
        float distance = Vector3.Distance(enemyController.transform.position, enemyController.player.position);
        if (distance <= enemyController.attackRange)
        {
            enemyController.enemyStateMachine.ChangeState(new MageAttackState(enemyController));
        }

    }
}
