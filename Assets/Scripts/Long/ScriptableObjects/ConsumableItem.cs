using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ConsumableItem : ItemBase
{
    public abstract void Use(GameObject targetObject);

}
