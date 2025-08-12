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
    public ItemBase item;
    public InventorySlot activeSlot;

    void Awake()
    {
        itemIcon = GetComponent<Image>();
        if (itemIcon == null)
        {
            Debug.LogError("Image component not found on InventoryItem.");
        }
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component not found on InventoryItem.");
        }
    }
    public void Initialize(ItemBase itemBase, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.item = this;
        item = itemBase;
        itemIcon.sprite = itemBase.itemIcon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //InventoryManager.Instance.SetCarriedItem(this);
        }
    }

}
