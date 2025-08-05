using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : EffectBase
{
    float damagePerSecond => effectConfig.parameters[0];
    GameObject assetPrefab => Resources.Load<GameObject>(effectConfig.asset);
    GameObject prefap;
    Coroutine burnCoroutine;
    public BurningEffect(SkillBase skillBase, BurningEffectConfig effectConfig, UnitBase owner = null)
    {
        this.skillBase = skillBase;
        this.owner = owner;
        this.effectConfig = effectConfig;
    }
    public override void OnActive(UnitBase onTarget)
    {
        Debug.Log("BurningEffect OnActive");
        SetUnitBase(onTarget);
        prefap = Object.Instantiate(assetPrefab, onTarget.transform.position + Vector3.down*0.5f, Quaternion.identity);
        burnCoroutine = onTarget.StartCoroutine(Burn());
    }
    private IEnumerator Burn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < effectConfig.duration)
        {
            owner.GetHealthComponent().OnTakeDmg(damagePerSecond * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            yield return null; 
        }
        OnDeactive(); 
    }
    public override void OnDeactive()
    {
        Debug.Log("BurningEffect OnDeactive");
        //assetPrefab.SetActive(false); 
        Object.Destroy(prefap);
        //Resources.UnloadAsset(assetPrefab);
        owner.StopCoroutine(burnCoroutine);
        owner.GetComponent<EffectManager>()?.RemoveEffect(this);
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
    }    
}
