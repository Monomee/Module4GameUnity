using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDieState : IEnemyState
{
    private BossEnemy boss;
    public BossDieState(Enemy enemyController)
    {
        boss = enemyController as BossEnemy;
    }


    public void Enter()
    {
        // Dừng agent 
        if (boss.agent != null)
        {
            if (boss.agent.enabled && boss.agent.isOnNavMesh)
            {
                boss.agent.isStopped = true; // thay cho Stop()
            }
            // Sau đó có thể disable hẳn agent
            boss.agent.enabled = false;
        }

        // Tắt animator để không giữ  xương
        if (boss.animator != null)
            boss.animator.enabled = false;

        // Bật ragdoll
        boss.EnableRagdoll();
        if (boss.gameObject.GetComponent<CapsuleCollider>() != null)
        {
            boss.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        Object.Destroy(boss.gameObject, 3f);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
