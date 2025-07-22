using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp { get; set; }
    public Health(float hp)
    {
        this.hp = hp;
    }

    public void OnTakeDmg(float damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;
    }
}
