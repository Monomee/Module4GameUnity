using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Health : MonoBehaviour
{
    public event System.Action Died;
    bool isPlayer;
    StatConfigBase hp;
    public float health;
    UnitBase owner;
    public void Awake()
    {
        hp = new StatConfigBase(StatType.HP, 500, 1, 0, 1, 0, 0, 500);
        if (GetComponent<Enemy>() != null)
        {
            if (GetComponent<Enemy>() is not BossEnemy)
            {
                hp = new StatConfigBase(StatType.HP, 100, 1, 0, 1, 0, 0, 100);
            }
        }
        AddListener();
        owner = GetComponent<UnitBase>();
        owner.AddStats(StatType.HP, hp);
        Debug.Log("HP: " + hp.GetValue());
        isPlayer = owner is Player;
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
        if (owner.isDead)
        {
            Debug.Log("Unit is dead");
            owner.animator.SetBool("Alive", false);
            RemoveListener();
            if (GetComponent<Player>() != null)
            {
                UIManager.Instance.ShowGameOverPanel();
            }
        }
    }

    
    public void OnTakeDmg(float damage)
    {
        // Trừ trước, rồi mới xét chết
        hp.AddValue(-damage);
        health = hp.value;

        if (hp.value <= 0)
        {
            hp.value = 0;
            
            if (!owner.isDead)
            {
                owner.isDead = true;
                Died?.Invoke(); // Thong bao cho Enemy
            }
        }
    }
}
