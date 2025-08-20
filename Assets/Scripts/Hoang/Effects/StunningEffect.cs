using StarterAssets;
using System.Collections;
using UnityEngine;

public class StunningEffect : EffectBase
{
    GameObject asset => Resources.Load<GameObject>(effectConfig.asset);
    GameObject stun;
    Coroutine stunCoroutine;

    public StunningEffect(UnitBase fromOwner, SkillBase fromSkill, EffectConfig effectConfig)
    {
        this.fromSkill = fromSkill;
        this.fromOwner = fromOwner;
        this.effectConfig = effectConfig;
    }

    public override void OnActive()
    {
        Debug.Log("BurningEffect OnActive");
        stun = Object.Instantiate(asset, target.transform.position + Vector3.down * 0.5f, Quaternion.identity);
        stunCoroutine = fromOwner.StartCoroutine(Stunning());
    }
    private IEnumerator Stunning()
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
            else if (target is TestEnemy)
            {
                target = (TestEnemy)target;
                target.GetComponent<TestEnemy>().enabled = false;
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
        Debug.Log("Stunning OnDeactive");
        Object.Destroy(stun);
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
        fromOwner.StopCoroutine(stunCoroutine);
        target.GetComponent<Animator>().speed = 1f;
        fromSkill.DeapplyEffect(this);
    }
}
public class StunningEffectConfig : EffectConfig
{
    public StunningEffectConfig()
    {
        codeName = "Stunning";
        duration = 5f;
        asset = "VFX/Magic effects pack/Prefabs/Hits and explosions/Electro hit";
        parameters = new float[] { };
        activeEvent = EffectActiveEvent.OnGetHit;
        targetType = TargetType.Enemy;
    }
}
