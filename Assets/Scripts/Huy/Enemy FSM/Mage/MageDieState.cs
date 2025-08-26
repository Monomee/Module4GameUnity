using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageDieState : IEnemyState
{
    private MageEnemy mage;
    public MageDieState(Enemy enemyController)
    {
        mage = enemyController as MageEnemy;
    }


    public void Enter()
    {
        // Dừng agent 
        if (mage.agent != null)
        {
            if (mage.agent.enabled && mage.agent.isOnNavMesh)
            {
                mage.agent.isStopped = true; // thay cho Stop()
            }
            // Sau đó có thể disable hẳn agent
            mage.agent.enabled = false;
        }

        // Tắt animator để không giữ  xương
        if (mage.animator != null)
            mage.animator.enabled = false;

        // Bật ragdoll
        mage.EnableRagdoll();
        if (mage.gameObject.GetComponent<CapsuleCollider>() != null)
        {
            mage.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
