using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{ 
    private KnightEnemy knight;
    private const string IDLE_ANIMATION_BOOL = "isIdle";


    public IdleState(Enemy enemyController)
    {
        knight = enemyController as KnightEnemy;
    }

    public void Enter()
    {
        knight.animator.SetBool(IDLE_ANIMATION_BOOL, true);
    }

    public void Update()
    {
        if(knight.DetectedPlayer())
        {
            knight.enemyStateMachine.ChangeState(new SwordDrawState(knight));
        }

    }

    public void Exit()
    { 
        knight.animator.SetBool(IDLE_ANIMATION_BOOL, false);
    }
}
