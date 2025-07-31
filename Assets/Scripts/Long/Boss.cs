using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Health>().Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
