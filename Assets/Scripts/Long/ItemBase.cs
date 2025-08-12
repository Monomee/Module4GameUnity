using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemBase : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    [TextArea]public string itemDescription;
    //[SerializeField]private int maxStackSize = 5; 
}


