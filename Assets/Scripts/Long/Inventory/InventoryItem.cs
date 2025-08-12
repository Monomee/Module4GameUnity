using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup;

    public ItemBase myItem;

    public InventorySlot activeSlot;
    public EquipmentSlot activeEquipSlot;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(ItemBase item, InventorySlot parent)
    {
        activeSlot = parent;
        activeEquipSlot = null;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.itemIcon;
    }

    public void Initialize(ItemBase item, EquipmentSlot parent)
    {
        activeEquipSlot = parent;
        activeSlot = null;
        activeEquipSlot.currentItem = this;
        myItem = item;
        itemIcon.sprite = item.itemIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (myItem is ConsumableItem consumable)
        {
            consumable.Use(InventoryManager.Instance.playerGameObject);
            if (activeSlot != null)
            {
                activeSlot.myItem = null;
            }
            else if (activeEquipSlot != null)
            {
                activeEquipSlot.Clear();
            }
            Destroy(gameObject);
            return;
        }

        InventoryManager.Instance.SetCarriedItem(this);
    }

}
