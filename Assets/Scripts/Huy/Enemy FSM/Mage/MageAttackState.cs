using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttackState : IEnemyState
{
    private Enemy enemyController;
    private const string ATTACK_ANIMATION_BOOL = "isAttack";

    public MageAttackState(Enemy enemyController)
    {
        this.enemyController = enemyController;
    }

    public void Enter()
    {
        enemyController.animator.SetBool(ATTACK_ANIMATION_BOOL, true);
    }

    public void Update()
    {
        if (enemyController.player == null) return; // ?
        float distance = Vector3.Distance(enemyController.transform.position, enemyController.player.position);

        if (distance > enemyController.attackRange + 0.5f)
        {
            enemyController.enemyStateMachine.ChangeState(new MageWalkState(enemyController));
            return;
        }
    }

    public void Exit()
    {
        enemyController.animator.SetBool(ATTACK_ANIMATION_BOOL, false);
    }
}
