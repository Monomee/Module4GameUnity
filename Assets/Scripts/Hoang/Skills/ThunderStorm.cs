using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStorm : SkillBase
{
    GameObject pointerPrefab => Resources.Load<GameObject>("Pointer");
    GameObject pointer;
    GameObject thunder;
    Vector3 point;
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[2];

    public ThunderStorm(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
        effects.Add(new StunningEffect(owner, this, new StunningEffectConfig()));
    }
    public override void OnActive(bool active)
    {
        if (active)
        {
            if (pointer == null)
            {
                pointer = Object.Instantiate(pointerPrefab);
            }
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (Mathf.Abs(Vector3.Dot(ray.direction, owner.transform.forward)) <= 0.9f)
                {
                    if (hit.collider != null)
                    {
                        point = hit.point;
                    }
                    else
                    {
                        point = ray.direction;
                    }
                    pointer.transform.position = point;
                }
                Object.Destroy(pointer, 10f);
            }
        }
        if (Input.GetKeyUp(KeyCode.E) && Time.time - lastCastTime >= cooldown)
        {
            Object.Destroy(pointer, 1f);
            lastCastTime = Time.time;

            owner.animator.SetTrigger("Attack");
            thunder = Object.Instantiate(skillPrefab, point, Quaternion.identity);

            OnStayCollideSkill script = thunder.GetComponent<OnStayCollideSkill>();
            if (script != null)
            {
                float damage = skillConfig.parameters[0];
                float duration = skillConfig.parameters[1];
                script.Initialize(owner, this, point, damage, duration);
            }
            else
            {
                Debug.LogError("OnStayCollideSkill component not found on the inferno prefab.");
            }
        }
    }
    public override void OnDeactive()
    {

    }
}
public class ThunderStormConfig: SkillConfig
{
    public ThunderStormConfig()
    {
        codeName = "ThunderStorm";
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        asset = "UsedAsset/ThunderStorm";
        parameters = new float[] { 20f, 8f, 20f }; // continuous damage, duration, cooldown
    }
}
