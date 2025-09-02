using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{ 
    public List<BoxCollider> doors;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (BoxCollider door in doors)
            {
                door.isTrigger = false;
            }
        }
    }
}
