using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ConsumableItem : ItemBase
{
    //public float cooldown = 0;
    public abstract void Use(GameObject targetObject);
}
