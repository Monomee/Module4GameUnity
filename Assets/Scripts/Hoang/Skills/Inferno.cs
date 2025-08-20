using System.Collections.Generic;
using UnityEngine;

public class Inferno : SkillBase
{
    GameObject pointerPrefab => Resources.Load<GameObject>("Pointer");
    GameObject pointer;
    GameObject inferno;
    Vector3 point;
    private GameObject skillPrefab => Resources.Load<GameObject>(skillConfig.asset);
    private float lastCastTime = -Mathf.Infinity;
    private float cooldown => skillConfig.parameters[2];

    public Inferno(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
        this.effects.Add(new BurningEffect(this.owner, this, new BurningEffectConfig()));
    }
    public override void OnActive(bool active)
    {        
        if (active)
        {
            if (pointer==null)
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
            inferno = Object.Instantiate(skillPrefab, point, Quaternion.identity);

            OnStayCollideSkill script = inferno.GetComponent<OnStayCollideSkill>();
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
public class InfernoConfig : SkillConfig
{
    public InfernoConfig()
    {
        codeName = "Inferno";
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        asset = "Hun0FX/FX/FireFX_vol1/Prefabs/FX_Fire_01";
        parameters = new float[] { 10f, 8f, 20f }; // continuous damage, duration, cooldown
    }
}
