using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class BossAttackState : IEnemyState
{
    private BossEnemy boss;
    private float waitUntil;

    public BossAttackState(Enemy enemyController)
    {
        boss = enemyController as BossEnemy;
    }

    public void Enter()
    {
        boss.StartSummon();
        waitUntil = Time.time + 0.7f; // telegraph + spawn time buffer
    }

    public void Update()
    {
        if (Time.time >= waitUntil && !boss.isCasting)
        {
            boss.enemyStateMachine.ChangeState(new BossIdleState(boss));
        }
    }
    public void Exit()
    {

    }
}
