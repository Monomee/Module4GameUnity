using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatConfigBase
{
    protected StatType statType;
    protected float baseValue;
    protected float basePercentValue;
    protected float otherValue;
    protected float allPercentValue;
    float outAllPercentValue;
    private Action onCheckMinValue;
    float minValue;
    float maxValue;

    public float value;

    public Action OnCheckMinValue { get => onCheckMinValue; set => onCheckMinValue = value; }

    public StatConfigBase(StatType statType, float baseValue, float basePercentValue, float otherValue, float allPercentValue, float outAllPercentValue, float minValue, float maxValue, Action onCheckMinValue = null)
    {
        this.statType = statType;
        this.baseValue = baseValue;
        this.basePercentValue = basePercentValue;
        this.otherValue = otherValue;
        this.allPercentValue = allPercentValue;
        this.outAllPercentValue = outAllPercentValue;
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.onCheckMinValue = onCheckMinValue;
    }
    public void Recalculate()
    {
        value = GetValue();
        if (value < minValue)
        {
            value = minValue;
            onCheckMinValue?.Invoke();
        }
        else if (value > maxValue)
        {
            value = maxValue;
        }
    }

    public float GetValue()
    {
        return (1 + basePercentValue * baseValue + otherValue) * allPercentValue + outAllPercentValue;
    }

    public void AddValue(float value)
    {
        otherValue += value;
        Recalculate();
    }





}
