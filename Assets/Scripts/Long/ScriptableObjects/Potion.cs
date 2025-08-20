using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "ScriptableObjects/Consumable/Potion")]
public class Potion : ConsumableItem
{
    public List<StatModifier> statModifiers;
    public EffectApplyType effectApplyType;
    public float effectDuration;
    public override void Use(GameObject targetObject)
    {
        if (targetObject == null)
            return;

        if (!targetObject.TryGetComponent<UnitBase>(out var unitBase))
            return;

        foreach (var modifier in statModifiers)
        {
            if (unitBase.roleStat.dictStats.ContainsKey(modifier.statType))
            {
                // Apply the potion effect
                unitBase.roleStat.ApplyModifier(modifier);

                // If the effect is temporary, start a coroutine to revert it after the duration
                if (effectApplyType == EffectApplyType.Temporary && effectDuration > 0)
                {
                    unitBase.StartCoroutine(RevertEffect(unitBase.roleStat, modifier, effectDuration));
                }
            }
        }
    }
    private IEnumerator RevertEffect(RoleStat roleStat, StatModifier modifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        roleStat.RemoveModifier(modifier);
    }

    public override ConsumableItem GetConsumableItem() { return this; }
}
