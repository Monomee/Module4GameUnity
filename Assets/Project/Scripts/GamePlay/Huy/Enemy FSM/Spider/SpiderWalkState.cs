using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWalkState : IEnemyState
{
    private SpiderEnemy spider;
    private const string SPIDER_WALK_ANIMATION_BOOL = "isWalk";
    // Start is called before the first frame update

    public SpiderWalkState(Enemy enemyController)
    {
        spider = enemyController as SpiderEnemy;
    }
    public void Enter()
    {
        spider.animator.SetBool(SPIDER_WALK_ANIMATION_BOOL, true);

    }

    public void Update()
    {
        spider.FaceTarget();
        spider.agent.SetDestination(spider.player.position);
        // Attack
        spider.CheckAndAttackPlayer();
    }

    public void Exit()
    {
        spider.animator.SetBool(SPIDER_WALK_ANIMATION_BOOL, false);
    }
}
