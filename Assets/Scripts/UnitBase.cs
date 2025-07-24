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
    public float hp;
    protected void HealthInit ()
    {
        if (healthComponent == null)
        {
            healthComponent = new Health(hp);
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
