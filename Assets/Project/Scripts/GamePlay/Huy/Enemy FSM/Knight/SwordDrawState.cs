using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDrawState : IEnemyState
{
    private Enemy enemyController;
    private const string SWORD_DRAWING_TRIGGER = "isDrawing";

    private float drawTime = 0.25f; // thời gian rút kiếm (khớp với animation)
    private float timer;

    public SwordDrawState(Enemy enemyController)
    {
        this.enemyController = enemyController;
    }
    public void Enter()
    {
        timer = drawTime;
        enemyController.animator.SetTrigger(SWORD_DRAWING_TRIGGER);
    }

    // Update is called once per frame
    public void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            enemyController.enemyStateMachine.ChangeState(new RunningState(enemyController));
        }

    }

    public void Exit()
    {
        // Trigger no need 
    }
}
