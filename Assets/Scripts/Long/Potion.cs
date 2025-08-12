using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "ScriptableObjects/Consumable/Potion")]
public class Potion : ConsumableItem
{
    //public PotionData potionData;
    public StatType statType;
    public float effectValue;
    public float effectDuration;
    public override void Use(GameObject targetObject)
    {
        if (targetObject == null)
            return;

        var roleStat = targetObject.GetComponent<RoleStat>();
        if (roleStat == null)
            return;

        if (roleStat.dictStats.ContainsKey(statType))
        {
            // Apply the potion effect
            roleStat.dictStats[statType].AddValue(effectValue);

            // If the potion has a duration, start a coroutine to revert the effect after the duration
            if (effectDuration > 0)
            {
                targetObject.GetComponent<MonoBehaviour>().StartCoroutine(RevertEffect(roleStat, statType, effectValue, effectDuration));
            }
        }
    }
    private IEnumerator RevertEffect(RoleStat roleStat, StatType statType, float value, float duration)
    {
        yield return new WaitForSeconds(duration);
        roleStat.dictStats[statType].AddValue(-value);
    }
}
