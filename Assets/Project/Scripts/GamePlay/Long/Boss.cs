using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : UnitBase
{
    public float moveSpeed;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        if (roleStat == null)
        {
            roleStat = new RoleStat();
        }
        roleStat.dictStats = new Dictionary<StatType, StatConfigBase>();
    }

}
