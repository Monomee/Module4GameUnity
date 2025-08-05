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

    public int teamID;
    RoleStat roleStat;
    List<SkillBase> skills;
    List<EffectBase> effects;
    protected Health healthComponent;
    protected Attack attackComponent;
    public float hp;
    protected virtual void HealthInit ()
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

    protected virtual void AttackInit()
    {
        if (attackComponent == null)
        {
            attackComponent = new Attack(); 
        }
    }

    public virtual void OnTakeDmg(float dmg)
    {
        if (healthComponent != null)
        {
            healthComponent.OnTakeDmg(dmg); 
        }
    }

    public bool IsDead()
    {
        return healthComponent?.hp <= 0;
    }
    public Health GetHealthComponent()
    {
        return healthComponent;

    }


    
}
