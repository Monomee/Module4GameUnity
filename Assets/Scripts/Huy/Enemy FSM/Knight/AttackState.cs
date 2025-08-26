using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    private KnightEnemy knight;
    private const string ATTACK_ANIMATION_BOOL = "isAttacking";


    public AttackState(Enemy enemyController)
    {
        knight = enemyController as KnightEnemy;
    }

    public void Enter()
    {
        knight.animator.SetBool(ATTACK_ANIMATION_BOOL, true);
    }


    public void Update()
    {
        if (knight.player == null) return;

        float distance = Vector3.Distance(knight.transform.position, knight.player.position);

        // Nếu player chạy xa → đuổi tiếp
        if (distance > knight.attackRange + 0.5f)
        {
            knight.readyToAttack = false;
            knight.enemyStateMachine.ChangeState(new RunningState(knight));
            return;
        }

        else
        {
            knight.isReadyToAttack = true;
        }
    }

    public void Exit()
    {
        knight.animator.SetBool(ATTACK_ANIMATION_BOOL, false);
    }
}
