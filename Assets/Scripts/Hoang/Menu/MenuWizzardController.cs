using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuWizzardController : MonoBehaviour
{
    public NavMeshAgent agent; 
    public Camera mainCamera; 
    public Animator animator;
    const string WALK = "isWalking";

    private void Start()
    {
        animator.SetBool(WALK, false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }           
        }
        if (agent.remainingDistance <= 0.1f)
        {
            animator.SetBool(WALK, false);
        }
        else
        {
            animator.SetBool(WALK, true);
        }
    }
}
