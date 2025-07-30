using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    public float moveSpeed;
    private void Awake()
    {
        HealthInit();
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            healthComponent.OnTakeDmg(10);
            Debug.Log("Enemy hit by projectile. Current HP: " + healthComponent.hp);
        }
    }
}
