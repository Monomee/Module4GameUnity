using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : SkillBase
{
    public Revive(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
    }
    public override void OnActive()
    {
        Debug.Log("Revive OnActive");
        owner.GetComponent<Health>().OnTakeDmg(-50f);
        ApplyEffects();
    }
    public override void OnDeactive()
    {
        Debug.Log("Revive OnDeactive");
    }
    protected override void ApplyEffects()
    {
        // Implement the logic to apply the revive effects
        Debug.Log("Applying revive effects");
    }
}
public class ReviveConfig : SkillConfig
{
    public ReviveConfig()
    {
        codeName = "Revive";
        activeCondition = SkillActiveCondition.OnDead;
        castType = SkillCastType.Passive;
        effects = new List<EffectConfig>();
    }
}
