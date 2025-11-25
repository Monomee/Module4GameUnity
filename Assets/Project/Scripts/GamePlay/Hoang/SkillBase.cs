using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase // can tao skillmanager ke thua mono behavior chay update new ()
{
    public UnitBase owner;
    public SkillConfig skillConfig;
    public List<EffectBase> effects = new List<EffectBase>();

    //tinh cooldown, virtual active , disable , 
    public virtual void OnActive(bool active)
    {
        ApplyEffects();
    }
    public virtual void OnDeactive()
    {

    }

    public void ApplyEffects()
    {
        owner.GetComponent<EffectManager>().AddListEffect(effects);
    }
    public void DeapplyEffect(EffectBase effect)
    {
        owner.GetComponent<EffectManager>().RemoveEffect(effect);
    }
}
