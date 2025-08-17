using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemSlot
{
    InventoryItem GetItem();

    void SetItem(InventoryItem newItem);

    void Clear();

    bool HasItem();
}
