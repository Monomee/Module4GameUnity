using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryManager.carriedItem == null) return;

            // InventorySlot ch? ch?a item bình th??ng (không ph?i EquipmentItem)
            if (InventoryManager.carriedItem.myItem is EquipmentItem)
                return;

            SetItem(InventoryManager.carriedItem);
        }
    }

    public void SetItem(InventoryItem inventoryItem)
    {
        InventoryManager.carriedItem = null;

        // N?u có item c?, clear activeSlot
        if (item != null)
        {
            item.activeSlot = null;
        }

        item = inventoryItem;
        item.activeSlot = this;
        item.activeEquipSlot = null;

        item.transform.SetParent(transform);
        item.canvasGroup.blocksRaycasts = true;
    }
}
