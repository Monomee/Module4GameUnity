using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "ScriptableObjects/EquipmentItem")]
public class EquipmentItem : ItemBase
{
    public EquipmentType equipmentType;
    [SerializeField] private List<StatModifier> statModifiers;
    public void Equip(GameObject targetObject)
    {
        ModifyStat(targetObject, apply: true);
    }
    public void Unequip(GameObject targetObject)
    {
        ModifyStat(targetObject, apply: false);
    }
    private void ModifyStat(GameObject targetObject, bool apply)
    {
        if (targetObject == null) return;
        if (!targetObject.TryGetComponent<UnitBase>(out var unitBase)) return;

        foreach (var modifier in statModifiers)
        {
            if (apply)
            {
                unitBase.roleStat.ApplyModifier(modifier);
            }
            else
            {
                unitBase.roleStat.RemoveModifier(modifier);
            }
        }
    }

    public override void Use(GameObject targetObject)
    {
        return;
    }

    public override EquipmentItem GetEquipmentItem() { return this; }
}
