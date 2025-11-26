using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SkillBase
{
    GameObject fireball;
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[0];
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);

    public FireBall(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
        this.effects.Add(new BurningEffect(this.owner, this, new BurningEffectConfig())); 
        this.effects.Add(new KnockbackEffect(owner, this, new KnockbackEffectConfig()));
    }
    public override void OnActive(bool active)
    {
        if (active && Time.time - lastCastTime >= cooldown)
        {
            Debug.Log("FireBall OnActive");
            lastCastTime = Time.time;

            owner.animator.SetTrigger("Attack");
            fireball = Object.Instantiate(skillPrefab);
            
            Vector3 direction = owner.transform.forward;

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
                            direction = (hit.point - owner.transform.position).normalized;
                        }
                        else
                        {
                            direction = ray.direction;
                        }
                    }
                }
            }

            OnEnterCollideSkill script = fireball.GetComponent<OnEnterCollideSkill>();
            if (script != null)
            {
                float damage = skillConfig.parameters[1];
                float duration = skillConfig.parameters[2];
                float speed = skillConfig.parameters[3];
                script.Initialize(owner, this, direction, owner.transform, damage, duration, speed);
            }

            //ApplyEffects();
        }
    }
    public override void OnDeactive()
    {       
        Debug.Log("FireBall OnDeactive");       
    }
}
public class FireBallConfig : SkillConfig
{
    public FireBallConfig()
    {
        codeName = "FireBall";
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        asset = "Hun0FX/FX/FireFX_vol1/Prefabs/FX_Fire_03"; 
        parameters = new float[] { 20f, 50f, 5f, 5f }; // cooldown, damage, duration, speed
    }
}
