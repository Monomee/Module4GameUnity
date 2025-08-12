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
    OnDead, //khi unitbase dead => goi skill manager check list skill xem co skill nao co active condition la OnDead khong, neu co thi goi skill do
    ASAP
}
public enum SkillCastType
{
    Passive,
    Active,
    AutoAimTarget,
    Custom
}
public enum EffectActiveEvent
{
    OnFirstTick,
    OnTickInterval,
    OnReceiveDamage,
    OnReceiveFatalDamage,
    OnFirstEngaging,
    OnNormalAttack,
    OnSendDmg,
    OnCheckHP,
    OnGetNormalAttack,
    OnTargetReceiveDamage,
    OnTargetCheckHPNormalAttack,
    OnKillTarget,
    OnGetHit,
    OnOtherDead,
    OnHeal,
    OnUseSkill,
    OnUseUltimateSkill, //hoangpl
    OnBeforeAfterDealDamage, // Call a pair
}
public enum TargetType
{
    Self,
    Ally,
    Enemy,
    All
}

public enum EffectApplyType
{
    Instant,
    Temporary
}

public enum EquipmentType
{
    Wand,
    Robe,
    Helmet,
    Boots
}

