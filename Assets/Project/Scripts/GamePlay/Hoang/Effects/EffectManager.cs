using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public List<EffectBase> effects;
    //public Action activeEffectAction;
    // Start is called before the first frame update
    public void Start()
    {
        //activeEffectAction += ActiveEffect;
        effects = new List<EffectBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (effects == null || effects.Count == 0) return;
        if (effects.Count > 0)
        {
            Debug.Log(effects.Count + " effects active.");
        }
    }
    public void ActiveEffect(String codeName, UnitBase target = null)
    {
        if (effects.Count == 0) return;
        for (int i = effects.Count - 1; i >= 0; i--)
        {
            if (effects[i] != null && effects[i].fromSkill.skillConfig.codeName.Equals(codeName))
            {
                switch (effects[i].effectConfig.targetType)
                {
                    case TargetType.All:
                        break;
                    case TargetType.Ally:
                        break;
                    case TargetType.Self:
                        effects[i].OnActive();
                        break;
                    case TargetType.Enemy:
                        EffectBase effectInstance = Activator.CreateInstance(effects[i].GetType(), effects[i].fromOwner, effects[i].fromSkill, effects[i].effectConfig) as EffectBase;
                        effectInstance.SetTarget(target);
                        effectInstance.OnActive();
                        break;
                }
            }
        }
        //foreach (EffectBase effect in effects)
        //{
        //    if (effect != null && effect.fromSkill.skillConfig.codeName.Equals(codeName))
        //    {
        //        switch (effect.effectConfig.targetType)
        //        {
        //            case TargetType.All:
        //                break;
        //            case TargetType.Ally:
        //                break;
        //            case TargetType.Self:
        //                effect.OnActive();
        //                break;
        //            case TargetType.Enemy:
        //                EffectBase effectInstance = Activator.CreateInstance(effect.GetType(), effect.fromOwner, effect.fromSkill, effect.effectConfig) as EffectBase;
        //                effectInstance.SetTarget(target);
        //                effectInstance.OnActive();
        //                break;
        //        }
        //    }
        //}
    }
    public void AddListEffect(List<EffectBase> effects)
    {
        if (this.effects == null)
        {
            this.effects = new List<EffectBase>();
        }
        foreach (var effect in effects)
        {
            if (!this.effects.Contains(effect))
            {
                this.effects.Add(effect);
            }
        }
    }
    public void AddEffect(EffectBase effect)
    {
        if (effects == null)
        {
            effects = new List<EffectBase>();
        }
        effects.Add(effect);
    }
    public void RemoveEffect(EffectBase effect)
    {
        var item = effects.FirstOrDefault(e => e.GetType() == effect.GetType());
        //if (effects != null && effects.Contains(effect))
        //{
        //    effects.Remove(effect);
        //}
        if (item != null)
        {
            effects.Remove(item);
        }
    }
    public void RemoveListEffect(List<EffectBase> effects)
    {
        if (this.effects != null)
        {
            foreach (var effect in effects)
            {
                if (this.effects.Contains(effect))
                {
                    this.effects.Remove(effect);
                }
            }
        }
    }
    public List<EffectBase> GetEffects() { return this.effects; }
   
}
