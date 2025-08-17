using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWalkState : IEnemyState
{
    private MageEnemy mage;
    private const string MAGE_WALK_ANIMATION_BOOL = "isWalk";

    public MageWalkState(Enemy enemyController)
    {
        mage = (MageEnemy)enemyController;
    }

    public void Enter()
    {
        mage.animator.SetBool(MAGE_WALK_ANIMATION_BOOL, true);
    }

    public void Update()
    {
        mage.agent.SetDestination(mage.player.position);
        mage.FaceTarget();

        // Attack
        mage.CheckAndAttackPlayer();
    }

    public void Exit()
    {
        mage.animator.SetBool(MAGE_WALK_ANIMATION_BOOL, false);
    }

}
