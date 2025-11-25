using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : IEnemyState
{
    private ArcherEnemy archer;
    private const string ARCHER_ATTACK_ANIMATION_BOOL = "isAttack";


    public ArcherAttackState(Enemy enemyController)
    {
        archer = enemyController as ArcherEnemy;
    }
    public void Enter()
    {
        archer.animator.SetBool(ARCHER_ATTACK_ANIMATION_BOOL, true);
    }

    // Update is called once per frame
    public void Update()
    {
        if (archer.player == null) return; // ?
        float distance = Vector3.Distance(archer.transform.position, archer.player.position);

        if (distance > archer.attackRange + 0.5f)
        {
            archer.isReadyToAttack = false;

            archer.enemyStateMachine.ChangeState(new ArcherWalkState(archer));
            return;
        }
        else
        {
            archer.isReadyToAttack = true;
        }
        archer.FaceTarget();


    }

    public void Exit()
    {
        archer.animator.SetBool(ARCHER_ATTACK_ANIMATION_BOOL, false);
    }
}
