using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : EffectBase
{
    GameObject asset => Resources.Load<GameObject>(effectConfig.asset);
    GameObject freeze;
    Coroutine freezeCoroutine;

    public FreezeEffect(UnitBase fromOwner, SkillBase fromSkill, EffectConfig effectConfig)
    {
        this.fromSkill = fromSkill;
        this.fromOwner = fromOwner;
        this.effectConfig = effectConfig;
    }

    public override void OnActive()
    {
        freeze = Object.Instantiate(asset, target.transform.position + Vector3.down * 0.5f, Quaternion.identity);
        freezeCoroutine = fromOwner.StartCoroutine(Freezing());
    }
    private IEnumerator Freezing()
    {
        float elapsedTime = 0f;

        while (elapsedTime < effectConfig.duration)
        {
            elapsedTime += Time.deltaTime;
            if (target is Player)
            {
                target = (Player)target;
                target.GetComponent<PlayerAttack>().enabled = false;
                target.GetComponent<ThirdPersonController>().enabled = false;
            }
            else if (target is Enemy)
            {
                target = (Enemy)target;
                target.GetComponent<Enemy>().enabled = false;
            }
            else if (target is Boss)
            {
                target = (Boss)target;
                target.GetComponent<Boss>().enabled = false;
            }
            target.GetComponent<Animator>().speed = 0f;
            yield return null;
        }       
        OnDeactive();
    }
    public override void OnDeactive()
    {
        Object.Destroy(freeze);
        if (target is Player)
        {
            target = (Player)target;
            target.GetComponent<PlayerAttack>().enabled = true;
            target.GetComponent<ThirdPersonController>().enabled = true;
        }
        else if (target is Enemy)
        {
            target = (Enemy)target;
            target.GetComponent<Enemy>().enabled = true;
        }
        else if (target is Boss)
        {
            target = (Boss)target;
            target.GetComponent<Boss>().enabled = true;
        }
        fromOwner.StopCoroutine(freezeCoroutine);
        target.GetComponent<Animator>().speed = 1f;
        fromSkill.DeapplyEffect(this);
    }
}
public class FreezeEffectConfig: EffectConfig
{
    public FreezeEffectConfig()
    {
        codeName = "Freeze";
        duration = 5f;
        asset = "VFX/Magic effects pack/Prefabs/Hits and explosions/Snow hit";
        parameters = new float[] { };
        activeEvent = EffectActiveEvent.OnGetHit;
        targetType = TargetType.Enemy;
    }
}
