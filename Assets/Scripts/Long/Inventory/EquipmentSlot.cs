using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentType equipType;
    public InventoryItem currentItem;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (InventoryManager.carriedItem == null)
            return;

        if (!(InventoryManager.carriedItem.myItem is EquipmentItem equipment) || equipment.equipmentType != equipType)
            return;

        SetItem(InventoryManager.carriedItem);
    }

    public void SetItem(InventoryItem newItem)
    {
        InventoryManager.carriedItem = null;

        if (currentItem != null && currentItem.myItem is EquipmentItem oldEquip)
        {
            oldEquip.Unequip(InventoryManager.Instance.playerGameObject);
            Destroy(currentItem.gameObject);
        }

        currentItem = newItem;
        currentItem.activeEquipSlot = this;
        currentItem.activeSlot = null;
        currentItem.transform.SetParent(transform);
        currentItem.canvasGroup.blocksRaycasts = true;

        if (currentItem.myItem is EquipmentItem newEquip)
        {
            newEquip.Equip(InventoryManager.Instance.playerGameObject);
        }
    }

    public void Clear()
    {
        if (currentItem != null && currentItem.myItem is EquipmentItem oldEquip)
        {
            oldEquip.Unequip(InventoryManager.Instance.playerGameObject);
            Destroy(currentItem.gameObject);
            currentItem = null;
        }
    }
}
