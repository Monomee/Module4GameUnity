using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : Enemy
{
    public Transform shootPoint;
    public ObjectPooling mageFireballPool;

    public float fireballSpeed = 10f;
    public float fireRate = 1f;

    protected override IEnemyState GetInitialState()
    {
        return new MageIdleState(this);
    }

    public override void CheckAndAttackPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
           
            enemyStateMachine.ChangeState(new MageAttackState(this));
        }

    }

}
