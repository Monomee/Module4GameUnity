using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float attackCooldown;

    public Attack(float damage, float attackRange, float attackSpeed, float attackCooldown)
    {
        this.damage = damage;
        this.attackRange = attackRange;
        this.attackSpeed = attackSpeed;
        this.attackCooldown = attackCooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
