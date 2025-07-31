using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : UnitBase
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
        GetComponent<Health>().Init();
        //roleStat.dictStats.Add(StatType.HP, new Health(StatType.HP, 1000, 0.5f, 0, 0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
