using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : Enemy
{
    public bool readyToAttack;
    public float spiderDamage = 5f;
    protected override IEnemyState GetInitialState()
    {
        return new SpiderIdleState(this);
    }

    public override void CheckAndAttackPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {

            enemyStateMachine.ChangeState(new SpiderAttackState(this));
        }

    }
}
