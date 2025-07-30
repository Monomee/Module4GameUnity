using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatConfigBase : MonoBehaviour
{
    protected StatType statType;
    protected float baseValue;
    protected float basePercentValue;
    protected float otherValue;
    protected float allPercentValue;

    //public float OtherValue { get => otherValue; set => otherValue = value; }

    public StatConfigBase(StatType statType, float baseValue, float basePercentValue, float otherValue, float allPercentValue)
    {
        this.statType = statType;
        this.baseValue = baseValue;
        this.basePercentValue = basePercentValue;
        this.otherValue = otherValue;
        this.allPercentValue = allPercentValue;
    }

    public float GetValue()
    {
        return (otherValue + (basePercentValue * baseValue)) * allPercentValue;
    }
}
