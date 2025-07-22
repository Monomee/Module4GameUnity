using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    public float damage;
    public float attackRange;
    public bool isDead;
    public float speed;
    public float attackSpeed;
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTakeDamge()
    { }

    public void Die()
    { }

    public void AttackComponent()
    { }

    public void GetHealthComponent()
    { }
}
