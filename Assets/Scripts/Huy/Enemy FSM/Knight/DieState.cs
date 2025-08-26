using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IEnemyState
{
    private KnightEnemy knight;
    public DieState(Enemy enemyController)
    {
        knight = enemyController as KnightEnemy;
    }


    public void Enter()
    {
        // Dừng agent 
        if (knight.agent != null)
        {
            if (knight.agent.enabled && knight.agent.isOnNavMesh)
            {
                knight.agent.isStopped = true; // thay cho Stop()
            }
            // Sau đó có thể disable hẳn agent
            knight.agent.enabled = false;
        }

        // Tắt animator để không giữ  xương
        if (knight.animator != null)
            knight.animator.enabled = false;

        // Bật ragdoll
        knight.EnableRagdoll();
        if (knight.gameObject.GetComponent<CapsuleCollider>() != null)
        {
            knight.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }
}
