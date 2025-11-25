using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;
    public int stackSize = 64;
    [TextArea]public string itemDescription;
    
    public virtual EquipmentItem GetEquipmentItem() { return null; }
    public virtual ConsumableItem GetConsumableItem() { return null; }
    public virtual ItemBase GetItem() { return this; }
    public abstract void Use(GameObject targetObject);
}


