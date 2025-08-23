using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : IEnemyState
{
    private ArcherEnemy archer;
    private const string ARCHER_IDLE_BOOL_ANIMATION = "isIdle";


    public ArcherIdleState(Enemy enemyController)
    {
        archer = (ArcherEnemy) enemyController;
    }
    // Start is called before the first frame update
    public void Enter()
    {
        archer.animator.SetBool(ARCHER_IDLE_BOOL_ANIMATION, true);
    }

    // Update is called once per frame
    public void Update()
    {
        if (archer.DetectedPlayer())
        {
            archer.enemyStateMachine.ChangeState(new ArcherWalkState(archer));
        }
    }

    public void Exit()
    {
        archer.animator.SetBool(ARCHER_IDLE_BOOL_ANIMATION, false);
    }
}
