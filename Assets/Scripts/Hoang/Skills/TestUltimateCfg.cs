using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUltimateCfg : SkillConfig
{
    
    
    public TestUltimateCfg()
    {
        range = 10f;
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        //skillPrefab = Resources.Load<GameObject>("Skills/UltimateSkillPrefab");
    }
    

}
