using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectConfig
{
    public string codeName;
    public float duration;
    public string asset; 
    public float[] parameters;
    public EffectActiveEvent activeEvent;//co active condition (dieu kien kich hoat)
    public TargetType targetType;  
}
