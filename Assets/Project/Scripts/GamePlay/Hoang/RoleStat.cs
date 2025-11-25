using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStat
{
    public Dictionary<StatType, StatConfigBase> dictStats;
    public RoleStat()
    {
        dictStats = new Dictionary<StatType, StatConfigBase>();
    }
}
