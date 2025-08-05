using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase
{
    protected SkillBase skillBase;
    protected UnitBase owner;
    protected EffectConfig effectConfig;
    
    public virtual void OnActive(UnitBase onTarget)
    {
        Debug.Log("EffectBase OnActive");
        // Apply effect logic here
    }
    public virtual void OnDeactive()
    {
        Debug.Log("EffectBase OnDeactive");
        // Clean up effect logic here
    }
    public void SetUnitBase(UnitBase unit)
    {
        owner = unit;
    }
}
