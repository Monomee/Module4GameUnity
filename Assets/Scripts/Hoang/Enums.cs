using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    
}

public enum StatType
{
    Atk,
    HP,
    Mana,
    Defense,
    Crit
}
public enum SkillActiveCondition
{
    OnAction,
    TargetIsEnemyInRange,
    TargetIsAllyInRange,
    TargetIsSelf,
    OnDead,
    ASAP
}
public enum SkillCastType
{
    Passive,
    Active,
    AutoAimTarget,
    Custom
}
