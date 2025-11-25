using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdleState : IEnemyState
{
    private SpiderEnemy spider;
    private const string IDLE_ANIMATION_BOOL = "isIdle";

    public SpiderIdleState(Enemy enemyController)
    {
        spider = enemyController as SpiderEnemy;
    }

    public void Enter()
    {
        spider.animator.SetBool(IDLE_ANIMATION_BOOL, true);
    }

    public void Update()
    {
        if (spider.DetectedPlayer())
        {
            spider.enemyStateMachine.ChangeState(new SpiderWalkState(spider));
        }

    }

    public void Exit()
    {
        spider.animator.SetBool(IDLE_ANIMATION_BOOL, false);
    }
}
