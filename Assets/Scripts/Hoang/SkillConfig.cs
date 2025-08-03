using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConfig
{
    public string codeName;
    public SkillActiveCondition activeCondition;
    public SkillCastType castType;
    public List<EffectConfig> effects;
    public string asset;
    public float[] parameters;
}
