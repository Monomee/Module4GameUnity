using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTrap : SkillBase
{
    GameObject trap;
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[0];
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);

    public TeslaTrap(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
    }
    public override void OnActive()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && Time.time - lastCastTime >= cooldown)
        {
            lastCastTime = Time.time;

            float radius = 5f; 
            for (int i = 0; i < skillConfig.parameters[3]; i++)
            {
                Vector3 randomPoint = owner.transform.position + Random.insideUnitSphere * radius;
                Vector3 spawnPoint = new Vector3(randomPoint.x, owner.transform.position.y, randomPoint.z);

                owner.animator.SetTrigger("Attack");
                trap = Object.Instantiate(skillPrefab, spawnPoint, Quaternion.identity);

                Vector3 projectileDir = owner.transform.position;

                OnStayCollideSkill script = trap.GetComponent<OnStayCollideSkill>();
                if (script != null)
                {
                    float damage = skillConfig.parameters[1];
                    float duration = skillConfig.parameters[2];
                    script.Initialize(owner, this, projectileDir, damage, duration);
                }
            }
            

            ApplyEffects();
        }
    }
    public override void OnDeactive()
    {

    }
}
public class TeslaTrapConfig: SkillConfig
{
    public TeslaTrapConfig() 
    {
        codeName = "TeslaTrap";
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        asset = "VFX/Magic effects pack/Prefabs/Character auras/Lightning aura";
        parameters = new float[] { 20f, 10f, 8f, 3 }; // cooldown, continuous damage, duration, numbers of traps
    }
}
