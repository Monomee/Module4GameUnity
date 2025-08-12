using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoleStatExtensions
{
    public static void ApplyModifier(this RoleStat roleStat, StatModifier modifier)
    {
        if (roleStat.dictStats.TryGetValue(modifier.statType, out var stat))
            stat.AddValue(modifier.value);
    }

    public static void RemoveModifier(this RoleStat roleStat, StatModifier modifier)
    {
        if (roleStat.dictStats.TryGetValue(modifier.statType, out var stat))
            stat.AddValue(-modifier.value);
    }
}
