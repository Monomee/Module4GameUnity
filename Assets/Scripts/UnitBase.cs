using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    public int teamID;
    RoleStat roleStat;
    List<SkillBase> skills;
    List<EffectBase> effects;
    protected Health healthComponent;
    protected Attack attackComponent;
    public float hp;
    protected virtual void HealthInit ()
    {
        if (healthComponent == null)
        {
            healthComponent = new Health(hp);
        }
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
