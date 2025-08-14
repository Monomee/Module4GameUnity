using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherWalkState : IEnemyState
{
    private ArcherEnemy archer;
    private const string ARCHER_WALK_ANIMATION_BOOL = "isWalk";

    public ArcherWalkState(Enemy enemyController)
    {
        archer = enemyController as ArcherEnemy;
    }
    public void Enter()
    {
        archer.animator.SetBool(ARCHER_WALK_ANIMATION_BOOL, true);
    }

    public void Update()
    {
        archer.agent.SetDestination(archer.player.position);
        archer.FaceTarget();
        // Attack
        archer.CheckAndAttackPlayer();
     
    }

    public void Exit()
    {
        archer.animator.SetBool(ARCHER_WALK_ANIMATION_BOOL, false);
    }
}
