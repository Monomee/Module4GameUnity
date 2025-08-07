using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : UnitBase
{
    Health health;
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (health == null)
        {
            health = GetComponent<Health>();
        }
    }

}
