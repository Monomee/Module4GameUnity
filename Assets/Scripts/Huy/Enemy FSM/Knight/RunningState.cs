using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IEnemyState
{
    private KnightEnemy knight;
    private const string RUNNING_ANIMATION_TRIGGER = "isRunning";
    // Start is called before the first frame update

    public RunningState(Enemy enemyController)
    {
        knight = enemyController as KnightEnemy;
    }
    public void Enter()
    {
        knight.animator.SetBool(RUNNING_ANIMATION_TRIGGER, true);

    }


    public void Update()
    {
        if (knight == null || knight.isDead)
            return;

        if (knight.agent != null && knight.agent.enabled && knight.agent.isOnNavMesh)
        {
            knight.FaceTarget();
            knight.agent.SetDestination(knight.player.position);
            knight.CheckAndAttackPlayer();
        }
    }

    public void Exit() 
    {
        knight.animator.SetBool(RUNNING_ANIMATION_TRIGGER, false);
    }

   
}
