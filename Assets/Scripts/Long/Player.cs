using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{ 
    Health health;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }        
        if (health == null)
        {
            health = GetComponent<Health>();
        }

        //Debug.Log("HP: " + roleStat.dictStats[StatType.HP].GetValue());
    }
    // Update is called once per frame
    void Update()
    {

    }


}

public class PlayerConfig : RoleConfig
{
    public PlayerConfig()
    {
        codeName = "Player";
        skillList = new List<string>();
        skillList.Add("TestUltimate");
        skillList.Add("Fireball");
    }
}