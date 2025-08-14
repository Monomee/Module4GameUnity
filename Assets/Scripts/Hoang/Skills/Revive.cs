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
        owner.roleStat.dictStats[StatType.HP].AddValue(50f);
        ApplyEffects();
    }
    public override void OnDeactive()
    {
        Debug.Log("Revive OnDeactive");
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
