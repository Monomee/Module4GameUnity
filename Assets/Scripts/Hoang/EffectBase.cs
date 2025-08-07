using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase
{
    public SkillBase fromSkill;
    public UnitBase fromOwner;
    public EffectConfig effectConfig;

    public UnitBase target;
    public void SetTarget(UnitBase target) { this.target = target; }
    public virtual void OnActive()
    {
        Debug.Log("EffectBase OnActive");
        // Apply effect logic here
    }
    public virtual void OnDeactive()
    {
        Debug.Log("EffectBase OnDeactive");
        // Clean up effect logic here
    }
}
