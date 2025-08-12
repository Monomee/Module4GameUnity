using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (InventoryManager.carriedItem == null) return;

        SetItem(InventoryManager.carriedItem);
    }

    public void SetItem(InventoryItem item)
    {
        InventoryManager.carriedItem = null;

        if (myItem != null)
            myItem.activeSlot = null;

        myItem = item;
        myItem.activeSlot = this;
        myItem.activeEquipSlot = null;

        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;
    }
}
