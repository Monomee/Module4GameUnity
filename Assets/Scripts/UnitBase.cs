using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    //public int teamID;
    public Animator animator;
    public RoleStat roleStat;   
    public bool isDead = false;
    public Health GetHealth()
    {
        return GetComponent<Health>();
    }
    public void AddStats(StatType type, StatConfigBase stat)
    {
        if (roleStat == null)
        {
            roleStat = new RoleStat();
        }
        roleStat.dictStats.Add(type, stat);
    }
    private void Start()
    {
        
    }
}
