using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    //public int teamID;
    public Animator animator;
    public RoleStat roleStat;   
    List<EffectBase> effects; // co the chuyen sang EffectManager
    public bool isDead = false;
    public GameObject Create(GameObject prefab)
    {
        return Instantiate(prefab);
    }
    public Health GetHealth()
    {
        return GetComponent<Health>();
    }
}
