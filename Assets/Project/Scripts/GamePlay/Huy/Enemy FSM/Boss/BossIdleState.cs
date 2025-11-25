using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IEnemyState
{
    private BossEnemy boss;
    private const string BOSS_IDLE_ANIMATION_BOOL = "isIdle";

    public BossIdleState(Enemy enemyController)
    {
        boss = enemyController as BossEnemy;   
    }


    public void Enter()
    {
        boss.animator.SetBool(BOSS_IDLE_ANIMATION_BOOL, true);
    }


    public void Update()
    {
        if (boss.player == null) return;

        float distance = Vector3.Distance(boss.transform.position, boss.player.position);
        boss.FaceTarget();

        if (boss.CanSwordRain())
        {
            boss.enemyStateMachine.ChangeState(new BossSwordRainState(boss));
            return;
        }

        if (boss.CanDefend() && distance >= 10)
        {
            boss.enemyStateMachine.ChangeState(new BossDefendState(boss));
            return;
        }

        if (boss.CanSummon() && distance <= 5)
        {
            boss.enemyStateMachine.ChangeState(new BossAttackState(boss));
            return;
        }

    }

    public void Exit()
    {
        boss.animator.SetBool(BOSS_IDLE_ANIMATION_BOOL, false);
    }
}
