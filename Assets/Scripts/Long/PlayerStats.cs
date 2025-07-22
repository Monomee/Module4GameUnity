using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitBase
{
    private void Awake()
    {
        HealthInit();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerStats initialized with HP: " + hp); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
