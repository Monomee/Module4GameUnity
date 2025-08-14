using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : Enemy
{
    public bool readyToAttack;
    public float knightDamage = 10f;
    protected override IEnemyState GetInitialState()
    {
        return new IdleState(this);
    } 
    
}
