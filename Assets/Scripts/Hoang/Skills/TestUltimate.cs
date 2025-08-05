using System.Collections.Generic;
using UnityEngine;

public class TestUltimate : SkillBase
{
    public TestUltimate(UnitBase owner, SkillConfig skillConfig, List<EffectBase> effects)
    {        
        this.owner = owner;
        this.skillConfig = skillConfig;
        this.effects = effects;
    }
    public override void OnActive()
    {
        Debug.Log("TestUltimate OnActive");
        if (Input.GetKeyDown(KeyCode.E))
        {
            owner.animator.SetTrigger("Attack");
            owner.animator.SetBool("Ultimate", true);
        }
        ApplyEffects();
    }
    public override void OnDeactive()
    {
        Debug.Log("TestUltimate OnDeactive");
        owner.animator.SetBool("Ultimate", false);
    }

    protected override void ApplyEffects()
    {
        
    }
}
