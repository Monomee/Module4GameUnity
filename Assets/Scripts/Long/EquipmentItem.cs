using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : ItemBase
{
    public EquipmentType equipmentType;
    public List<StatModifier> statModifiers;
    public void Equip(GameObject targetObject)
    {
        if (targetObject == null)
            return;
        var roleStat = targetObject.GetComponent<RoleStat>();
        if (roleStat == null)
            return;
        // Apply stat modifiers
        foreach (var modifier in statModifiers)
        {
            if (roleStat.dictStats.ContainsKey(modifier.statType))
            {
                roleStat.dictStats[modifier.statType].AddValue(modifier.value);
            }
        }
    }
}
