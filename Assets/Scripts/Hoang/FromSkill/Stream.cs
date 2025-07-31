using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CanTakeDmg"))
        {
            //((Health)other.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP]).OnTakeDmg(5);
            //Debug.Log(other.name + " is taking damage from the stream. Current HP: " + ((Health)other.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP]).HP);
        }
    }
}
