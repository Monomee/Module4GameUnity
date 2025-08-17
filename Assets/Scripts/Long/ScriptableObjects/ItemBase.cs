using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;
    [TextArea]public string itemDescription;
    
    public abstract EquipmentItem GetEquipmentItem();
    public abstract ConsumableItem GetConsumableItem();
    public abstract ItemBase GetItem();
}


