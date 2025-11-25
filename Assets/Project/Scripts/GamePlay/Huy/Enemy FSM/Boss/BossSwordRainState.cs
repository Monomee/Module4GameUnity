using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwordRainState : IEnemyState
{
    private BossEnemy boss;
    private float exitAt;


    public BossSwordRainState(Enemy enemyController)
    {
        boss = enemyController as BossEnemy;
    }

    public void Enter()
    {
        boss.StartSwordRain();
        // cast ends shortly after telegraph; buffer a bit
        exitAt = Time.time + boss.telegraphSwordTime + 0.1f;
    }
    public void Update()
    {
        if (Time.time >= exitAt && !boss.isCasting)
        {
            boss.enemyStateMachine.ChangeState(new BossIdleState(boss));
        }
    }
    public void Exit()
    {
        // nothing
    }
}