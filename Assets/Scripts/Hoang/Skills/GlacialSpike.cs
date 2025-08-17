using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlacialSpike : SkillBase
{
    GameObject spike;
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[0];
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);

    public GlacialSpike(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
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
            spike = Object.Instantiate(skillPrefab);

            Vector3 projectileDir = owner.transform.forward;

            if (owner.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                Ray ray = Camera.main.ScreenPointToRay(screenCenter);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (Mathf.Abs(Vector3.Dot(ray.direction, owner.transform.forward)) <= 0.9f)
                    {
                        if (hit.collider != null)
                        {
                            projectileDir = (hit.point - owner.transform.position).normalized;
                        }
                        else
                        {
                            projectileDir = ray.direction;
                        }
                    }
                }
            }
            
            OnEnterCollideSkill script = spike.GetComponent<OnEnterCollideSkill>();
            if (script != null)
            {
                float damage = skillConfig.parameters[1];
                float duration = skillConfig.parameters[2];
                script.Initialize(owner, this, projectileDir, owner.transform, damage, duration, 0);
            }

            ApplyEffects();
        }
    }
    public override void OnDeactive()
    {
        
    }
}
public class GlacialSpikeConfig: SkillConfig
{
    public GlacialSpikeConfig() 
    {
        codeName = "GlacialSpike";
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        asset = "UsedAsset/CrystalsFrontAttack";
        parameters = new float[] { 10f, 30f, 1.2f}; //cooldown, dmg, duration
    }
}
