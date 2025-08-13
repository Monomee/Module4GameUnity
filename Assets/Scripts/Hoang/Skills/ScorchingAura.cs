using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchingAura : SkillBase
{
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[0];
    GameObject zone;
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);
    public ScorchingAura(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
        this.effects.Add(new BurningEffect(this.owner, this, new BurningEffectConfig()));
    }
    public override void OnActive()
    {
        if (Time.time - lastCastTime >= cooldown)
        {
            lastCastTime = Time.time;
            
            if (owner.roleStat.dictStats.ContainsKey(StatType.Atk))
            {
                owner.roleStat.dictStats[StatType.Atk].AddValue(skillConfig.parameters[3]);
            }

            zone = Object.Instantiate(skillPrefab, owner.transform);
            OnStayCollideSkill script = zone.GetComponent<OnStayCollideSkill>();
            if (script != null)
            {
                script.Initialize(owner, this, owner.transform.position, skillConfig.parameters[1], 5);
            }

            ApplyEffects();
            owner.StartCoroutine(DeactivateAfterDelay());
        }
    }
    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(skillConfig.parameters[2]);
        OnDeactive();
        yield return null;
    }
    public override void OnDeactive()
    {
        if (owner.roleStat.dictStats.ContainsKey(StatType.Atk))
        {
            owner.roleStat.dictStats[StatType.Atk].AddValue(-skillConfig.parameters[3]);
        }
    }
    public override void ApplyEffects()
    {
        owner.GetComponent<EffectManager>().AddListEffect(effects);
    }
    public override void DeapplyEffect()
    {
        owner.GetComponent<EffectManager>().RemoveListEffect(effects);
    }
}
public class ScorchingAuraConfig: SkillConfig
{
    public ScorchingAuraConfig()
    {
        codeName = "ScorchingAura";
        activeCondition = SkillActiveCondition.ASAP;
        castType = SkillCastType.Passive;
        effects = new List<EffectConfig>();
        asset = "VFX/Magic effects pack/Prefabs/AoE effects/Meteors AOE";
        parameters = new float[] {60f, 5f, 15f, 10f, 10f, 10f }; //cooldown, dmg, duration, boost dmg, speed, def
    }
}
