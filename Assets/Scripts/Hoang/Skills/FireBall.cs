using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using UnityEngine;

public class FireBall : SkillBase
{
    GameObject fireball;
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[3];
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);

    public FireBall(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
        this.effects.Add(new BurningEffect(this, new BurningEffectConfig())); 
    }
    public override void OnActive()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && Time.time - lastCastTime >= cooldown)
        {
            Debug.Log("FireBall OnActive");
            lastCastTime = Time.time;

            owner.animator.SetTrigger("Attack");
            fireball = Object.Instantiate(skillPrefab);
            
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

            Projectile projectile = fireball.GetComponent<Projectile>();
            if (projectile != null)
            {
                float damage = skillConfig.parameters[0];
                float duration = skillConfig.parameters[1];
                float speed = skillConfig.parameters[2];
                projectile.Initialize(owner, this, projectileDir, owner.transform, damage, duration, speed);
            }

            ApplyEffects();
        }
    }
    public override void OnDeactive()
    {
        //owner.GetComponent<EffectManager>().RemoveListEffect(effects);
        Debug.Log("FireBall OnDeactive");       
    }
    protected override void ApplyEffects()
    {
        // Implement the logic to apply fireball effects
        //owner.GetComponent<EffectManager>().AddListEffect(effects);
        Debug.Log("Applying fireball effects");
    }
    public List<EffectBase> GetEffects()
    {
        return effects;
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
        parameters = new float[] { 30f, 5f, 5f, 20f }; // damage, duration, speed, cooldown
    }
}
