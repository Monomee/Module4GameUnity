using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
{
    public float moveSpeed;
    private void Awake()
    {
        HealthInit();
        AttackInit();
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
