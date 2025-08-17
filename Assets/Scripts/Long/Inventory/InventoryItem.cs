using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class InventoryItem
{
    [SerializeField]private ItemBase item;
    [SerializeField]private int quantity;

    public InventoryItem()
    {
        this.item = null;
        this.quantity = 0;
    }
    public InventoryItem(ItemBase item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
    public InventoryItem(InventoryItem inventoryItem)
    {
        this.item = inventoryItem.item;
        this.quantity = inventoryItem.quantity;
    }
    public ItemBase GetItem() { return item; }
    public int GetQuantity() { return quantity; }
    public void AddQuantity(int quantity) { this.quantity += quantity; }
    public void SubtractQuantity(int quantity)
    {
        this.quantity -= quantity;
        if (this.quantity < 0) this.quantity = 0;
    }

    public void AddItem(ItemBase item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void Clear()
    {
        this.item = null;
        this.quantity = 0;
    }

}
