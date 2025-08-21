using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    StatConfigBase hp;
    public float health;
    public void Start()
    {
        hp = new StatConfigBase(StatType.HP, 100, 1, 0, 1, 0, 0, 100);
        AddListener();
        GetComponent<UnitBase>().AddStats(StatType.HP, hp);
        Debug.Log("HP: " + hp.GetValue());
    }
    private void AddListener()
    {
        if (hp != null)
        {
            hp.OnCheckMinValue += OnChecKDead;
        }          
    }

    void RemoveListener()
    {
        if (hp != null)
        {
            hp.OnCheckMinValue -= OnChecKDead;
        }
    }
    public void OnChecKDead()
    {
        if (GetComponent<UnitBase>().isDead)
        {
            Debug.Log("Unit is dead");
            GetComponent<UnitBase>().animator.SetBool("Alive", false);
            RemoveListener();
        }
    }
    public void OnTakeDmg(float damage)
    {     
        if (hp.value <= 0)
        {
            hp.value = 0;
            GetComponent<UnitBase>().isDead = true;
        }
        hp.AddValue(-damage);
        health = hp.value;
    }
}
