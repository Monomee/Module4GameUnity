using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : Enemy
{
    public Transform shootPoint;
    public EnemyObjectPooling archerArrowPool;
    public float arrowSpeed = 30f;

    protected override IEnemyState GetInitialState()
    {
        return new ArcherIdleState(this);
    }

    public override void CheckAndAttackPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            FaceTarget();
            enemyStateMachine.ChangeState(new ArcherAttackState(this));
        }
    }

    protected override void HandleDied()
    {
        if (_hasDied) return;
        _hasDied = true;
        enemyStateMachine.ChangeState(new ArcherDieState(this));
    }
}
