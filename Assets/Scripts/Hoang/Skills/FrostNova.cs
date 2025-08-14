using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostNova : SkillBase
{
    GameObject nova;
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[0];
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);

    public FrostNova(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
        effects.Add(new FreezeEffect(owner, this, new FreezeEffectConfig()));
    }
    public override void OnActive()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && Time.time - lastCastTime >= cooldown)
        {
            lastCastTime = Time.time;

            owner.animator.SetTrigger("Attack");
            nova = Object.Instantiate(skillPrefab);

            Vector3 projectileDir = owner.transform.position;

            OnStayCollideSkill script = nova.GetComponent<OnStayCollideSkill>();
            if (script != null)
            {
                float damage = skillConfig.parameters[1];
                float duration = skillConfig.parameters[2];
                script.Initialize(owner, this, projectileDir, damage, duration);
            }

            ApplyEffects();
        }
    }
    public override void OnDeactive()
    {

    }
}
public class FrostNovaConfig: SkillConfig
{
    public FrostNovaConfig() 
    {
        codeName = "FrostNova";
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        asset = "Spells Pack 2 Free Version/Prefabs/Ice/Spell_Ice_1";
        parameters = new float[] { 20f, 10f, 6f }; //cooldown, dmg per sec, duration
    }
}