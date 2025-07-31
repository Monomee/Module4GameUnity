using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase
{
    SkillBase skillBase;
    UnitBase owner;
    public EffectConfig effectConfig;
    
    public void OnActive()
    {
        Debug.Log("EffectBase OnActive");
        // Apply effect logic here
    }
    public void OnDeactive()
    {
        Debug.Log("EffectBase OnDeactive");
        // Clean up effect logic here
    }
}
