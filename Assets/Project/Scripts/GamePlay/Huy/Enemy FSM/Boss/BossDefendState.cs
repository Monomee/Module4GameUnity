using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefendState : IEnemyState
{
    private BossEnemy boss;
    private const string BOSS_DEFEND_ANIMATION_BOOL = "isDefend";
    private Coroutine routine;

    public BossDefendState(Enemy enemyController)
    {
        boss = enemyController as BossEnemy;
    }

    public void Enter()
    {
        boss.animator.SetBool(BOSS_DEFEND_ANIMATION_BOOL, true);
        routine = boss.DoDefend();
    }

    public void Update()
    {
        boss.FaceTarget();
        // Return to Idle as soon as the defend window ends
        if (!boss.isDefending)
        {
            boss.enemyStateMachine.ChangeState(new BossIdleState(boss));
        }
    }
    public void Exit()
    {
        boss.animator.SetBool(BOSS_DEFEND_ANIMATION_BOOL, false);
    }
}

