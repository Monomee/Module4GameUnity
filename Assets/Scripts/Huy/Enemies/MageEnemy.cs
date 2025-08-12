using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : Enemy
{
    public Transform shootPoint;
    public TestPool mageFireballPool;

    public float fireballSpeed = 10f;
    public float fireRate = 1f;

    protected override IEnemyState GetInitialState()
    {
        return new MageIdleState(this); 
    }
}
