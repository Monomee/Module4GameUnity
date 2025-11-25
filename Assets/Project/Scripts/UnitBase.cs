using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{

    public Animator animator;
    public RoleStat roleStat;   
    public bool isDead = false;
  
    protected Health healthComponent;
    protected Attack attackComponent;

    public void AddStats(StatType type, StatConfigBase stat)
    {
        if (roleStat == null)
        {
            roleStat = new RoleStat();
        }
        roleStat.dictStats.Add(type, stat);
    }

    public Health GetHealthComponent()
    {
        return GetComponent<Health>();

    }


    
}
