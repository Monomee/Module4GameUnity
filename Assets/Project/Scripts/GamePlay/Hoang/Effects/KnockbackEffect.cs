using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : EffectBase
{
    GameObject asset => Resources.Load<GameObject>(effectConfig.asset);
    GameObject knock;

    public KnockbackEffect(UnitBase fromOwner, SkillBase fromSkill, EffectConfig effectConfig)
    {
        this.fromSkill = fromSkill;
        this.fromOwner = fromOwner;
        this.effectConfig = effectConfig;
    }

    public override void OnActive()
    {
        Debug.Log("BurningEffect OnActive");
        knock = Object.Instantiate(asset, target.transform.position + Vector3.down * 0.5f, Quaternion.identity);
        Object.Destroy(knock, 1.5f);
        //target.GetComponent<Rigidbody>().AddForce(0, 0, -effectConfig.parameters[0], ForceMode.Impulse);
        target.transform.position -= target.transform.forward * effectConfig.parameters[0];       
        target.StartCoroutine(Deactive());
    }
    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(3f);
        OnDeactive();
        yield break;
    }
    public override void OnDeactive()
    {        
        if (target is Player)
        {
            target = (Player)target;
            target.GetComponent<PlayerAttack>().enabled = true;
            target.GetComponent<ThirdPersonController>().enabled = true;
        }
        else if (target is TestEnemy)
        {
            target = (TestEnemy)target;
            target.GetComponent<TestEnemy>().enabled = true;
        }
        else if (target is Boss)
        {
            target = (Boss)target;
            target.GetComponent<Boss>().enabled = true;
        }
        fromSkill.DeapplyEffect(this);
    }
}
public class KnockbackEffectConfig: EffectConfig
{
    public KnockbackEffectConfig()
    {
        codeName = "Knockback";
        duration = 5f;
        asset = "VFX/Magic effects pack/Prefabs/Hits and explosions/Stones hit";
        parameters = new float[] { 5f }; //distance knockback
        activeEvent = EffectActiveEvent.OnGetHit;
        targetType = TargetType.Enemy;
    }
}
