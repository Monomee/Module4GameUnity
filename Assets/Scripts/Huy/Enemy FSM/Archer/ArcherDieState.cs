using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDieState : IEnemyState
{
    private ArcherEnemy archer;
    public ArcherDieState(Enemy enemyController)
    {
        archer = enemyController as ArcherEnemy;
    }


    public void Enter()
    {
        // Dừng agent 
        if (archer.agent != null)
        {
            if (archer.agent.enabled && archer.agent.isOnNavMesh)
            {
                archer.agent.isStopped = true; // thay cho Stop()
            }
            // Sau đó có thể disable hẳn agent
            archer.agent.enabled = false;
        }

        // Tắt animator để không giữ  xương
        if (archer.animator != null)
            archer.animator.enabled = false;

        // Bật ragdoll
        archer.EnableRagdoll();
        if (archer.gameObject.GetComponent<CapsuleCollider>() != null)
        {
            archer.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
