using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : UnitBase
{
    public float moveSpeed;
    public float attackRange = 2f;
    public float turnSpeed;
    public float detectionRadius = 5f;


    public bool hasDetectedPlayer = false;
    public bool isReadyToAttack;
    protected bool _hasDied;

    public Transform player;

    public NavMeshAgent agent;
    public EnemyStateMachine enemyStateMachine;

    private Rigidbody[] _ragdollRigidbodies;

    private void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //performace issue if too many object in the scene
        if (roleStat == null)
        {
            roleStat = new RoleStat();
        }
        roleStat.dictStats = new Dictionary<StatType, StatConfigBase>();

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        enemyStateMachine = new EnemyStateMachine();
        enemyStateMachine.ChangeState(GetInitialState());
;

    }

    void OnEnable()
    {
        var h = GetComponent<Health>();
        if (h != null) h.Died += HandleDied;
    }

    void OnDisable()
    {
        var h = GetComponent<Health>();
        if (h != null) h.Died -= HandleDied;
    }

    // Update is called once per frame
    void Update()
    {

        if (!_hasDied)
        {
            enemyStateMachine.Update();
        }
           
    }

    protected virtual IEnemyState GetInitialState()
    {
        return new IdleState(this); 
    }

    protected virtual void HandleDied()
    {
        if (_hasDied) return;
        _hasDied = true;
        enemyStateMachine.ChangeState(new DieState(this));
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
                                                                                                     //transform.rotation = lookRotation;   // Without slerp the enemy will immediately rotate so its not naturally.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        // transform.rotation = where the target is, we need to rotate at a certain speed

    }

    public virtual void CheckAndAttackPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            FaceTarget();
            enemyStateMachine.ChangeState(new AttackState(this));
        }
    }

    //protected virtual void OnDied()
    //{
    //    if(isDead == true)
    //    {
    //        enemyStateMachine.ChangeState(new DieState(this));
    //    }
    //}

    public void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

}
