using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    [SerializeField]private int maxStackSize = 5; 
}


