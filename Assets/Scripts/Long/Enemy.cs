using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : UnitBase
{
    public float moveSpeed;
    public float attackRange = 2f;
    public float turnSpeed;
    public float detectionRadius = 10f;

    public bool hasDetectedPlayer = false;

    public Transform player;

    public NavMeshAgent agent;
    public Animator animator;
    public EnemyStateMachine enemyStateMachine;

    private void Awake()
    {
        HealthInit();
        AttackInit();
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        enemyStateMachine = new EnemyStateMachine();
        enemyStateMachine.ChangeState(new IdleState(this));

        Debug.Log("EnemyStats initialized with HP: " + hp + " and Move Speed: " + moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        enemyStateMachine.Update();
    }

    public bool DetectedPlayer()
    {
        if (player == null || hasDetectedPlayer) return false;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRadius)
        {
            Debug.Log("Player detected!");
            hasDetectedPlayer = true;
            return true;
        }

        return false;
    }

    public void FaceTarget()
    {
        // Take the direction of the target
        Vector3 direction = (player.position - transform.position).normalized; // use normalized to calculate the enemy direction without affected by the distance
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // y=0 because we don't want the enemy looks up or down
                                                                                                     //transform.rotation = lookRotation;   // Withour slerp the enemy will immediately rotate so its not naturally.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        // transform.rotation = where the target is, we need to rotate at a certain speed
    }
}
