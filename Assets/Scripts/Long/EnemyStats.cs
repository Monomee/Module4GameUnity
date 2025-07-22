using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : UnitBase
{
    protected Attack attackComponent;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float attackCooldown;
    private void Awake()
    {
        HealthInit();
        if (attackComponent == null)
        {
            attackComponent = new Attack(damage, attackRange, attackSpeed, attackCooldown);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("EnemyStats initialized with HP: " + hp + " and Move Speed: " + moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
