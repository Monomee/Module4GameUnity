using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    protected UnitBase owner;
    protected SkillInfo skillInfo;
    protected SkillConfig skillConfig;
   
    protected GameObject skillPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetSkillPrefab()
    {
        return skillPrefab;
    }
    public void SetSkillPrefab(GameObject prefab)
    {
        skillPrefab = prefab;
    }
}
