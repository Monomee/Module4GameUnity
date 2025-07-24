using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatConfigBase : MonoBehaviour
{
    StatType statType;
    float baseValue;
    float basePercentValue;
    float otherValue;
    float allPercentValue;

    public float OtherValue { get => otherValue; set => otherValue = value; }
     
    public float GetValue()
    {
        return (otherValue + (basePercentValue * baseValue)) * allPercentValue;
    }
}
