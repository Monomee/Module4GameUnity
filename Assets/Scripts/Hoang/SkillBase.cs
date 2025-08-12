using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase // can tao skillmanager ke thua mono behavior chay update new ()
{
    public UnitBase owner;
    public SkillConfig skillConfig;
    public List<EffectBase> effects = new List<EffectBase>();

    //tinh cooldown, virtual active , disable , 
    public virtual void OnActive()
    {
        ApplyEffects();
    }
    public virtual void OnDeactive()
    {

    }

    protected virtual void ApplyEffects()
    {

    }
    public virtual void DeapplyEffect()
    {

    }
}
