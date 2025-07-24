using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    [SerializeField] private Animator animator;
    private void Awake()
    {
        HealthInit();
        AttackInit();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        Debug.Log("PlayerStats initialized with HP: " + hp); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            attackComponent.DoAttack(this.transform, TestPool.SharedInstance.GetPooledObject().GetComponent<Projectile>());
        }
    }
}
