using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TestUltimate : SkillBase
{
    GameObject ul;
    public TestUltimate(SkillInfo skillInfo, SkillConfig skillConfig, UnitBase owner)
    {
        this.skillInfo = skillInfo;
        this.skillConfig = skillConfig;
        this.owner = owner;
    }
    public void Cast()
    {
        //if (skillConfig.activeCondition == SkillActiveCondition.OnAction)
        //{
            Debug.Log($"{owner} casts Fireball with range {skillConfig.range}");
            ul = Instantiate(skillPrefab);
            ul.transform.position = owner.transform.position + Vector3.up + Vector3.forward; 
        ul.transform.rotation = Quaternion.LookRotation(-owner.transform.right);
        ul.transform.SetParent(owner.transform);
        ApplyEffects();
        //}
    }
    public void EndCast()
    {
        ul.transform.SetParent(null);
        Destroy(ul);
    }

    private void ApplyEffects()
    {
        Debug.Log($"Applying effects for skill {skillInfo.codeName}");
    }
}
