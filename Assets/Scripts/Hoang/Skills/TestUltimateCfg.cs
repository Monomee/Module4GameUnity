using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUltimateCfg : SkillConfig
{  
    public TestUltimateCfg()
    {
        codeName = "TestUltimate";
        activeCondition = SkillActiveCondition.OnAction;
        castType = SkillCastType.Active;
        effects = new List<EffectConfig>();
        skillPrefab = Resources.Load<GameObject>("Hun0FX/FX/FireFX_vol1/Prefabs/FX_Fire_06");
    }
}
