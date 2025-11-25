using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackState : IEnemyState
{
    private SpiderEnemy spider;
    private const string SPIDER_ATTACK_ANIMATION_BOOl = "isAttack";
    // Start is called before the first frame update

    public SpiderAttackState(Enemy enemyController)
    {
        spider = enemyController as SpiderEnemy;
    }
    public void Enter()
    {
        spider.animator.SetBool(SPIDER_ATTACK_ANIMATION_BOOl, true);

    }

    public void Update()
    {
        if (spider.player == null) return;

        float distance = Vector3.Distance(spider.transform.position, spider.player.position);

        // Nếu player chạy xa → đuổi tiếp
        if (distance > spider.attackRange + 0.5f)
        {
            spider.readyToAttack = false;
            spider.enemyStateMachine.ChangeState(new SpiderWalkState(spider));
            return;
        }

        else
        {
            spider.isReadyToAttack = true;
        }
    }

    public void Exit()
    {
        spider.animator.SetBool(SPIDER_ATTACK_ANIMATION_BOOl, false);
    }
}
