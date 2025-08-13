using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : EffectBase
{
    float damagePerSecond => effectConfig.parameters[0];
    GameObject assetPrefab => Resources.Load<GameObject>(effectConfig.asset);
    GameObject prefap;
    Coroutine burnCoroutine;
    
    public BurningEffect(UnitBase fromOwner, SkillBase fromSkill, BurningEffectConfig effectConfig)
    {
        this.fromSkill = fromSkill;
        this.fromOwner = fromOwner;
        this.effectConfig = effectConfig;
    }
    
    public override void OnActive()
    {
        Debug.Log("BurningEffect OnActive");
        prefap = Object.Instantiate(assetPrefab, target.transform.position + Vector3.down*0.5f, Quaternion.identity); 
        burnCoroutine = fromOwner.StartCoroutine(Burn());
    }
    private IEnumerator Burn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < effectConfig.duration)
        {
            target.GetHealthComponent().OnTakeDmg(damagePerSecond * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        OnDeactive(); 
    }
    public override void OnDeactive()
    {
        Debug.Log("BurningEffect OnDeactive");
        Object.Destroy(prefap);
        fromOwner.StopCoroutine(burnCoroutine);
        fromSkill.DeapplyEffect();
    }
}
public class BurningEffectConfig : EffectConfig
{
    public BurningEffectConfig()
    {
        codeName = "Burning";
        duration = 5f; 
        asset = "Hun0FX/FX/FireFX_vol1/Prefabs/FX_Fire_04";
        parameters = new float[] { 10f }; // damage per second
        activeEvent = EffectActiveEvent.OnGetHit;
        targetType = TargetType.Enemy;
    }    
}
