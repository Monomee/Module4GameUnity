using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;

    [SerializeField] EquipmentSlot[] equipmentSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    public GameObject playerGameObject;

    [Header("Item List")]
    [SerializeField] ItemBase[] items;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem targetItem)
    {
        if (carriedItem != null)
        {
            if (targetItem.activeSlot != null)
            {
                targetItem.activeSlot.SetItem(carriedItem);
            }
            else if (targetItem.activeEquipSlot != null)
            {
                if (carriedItem.myItem is EquipmentItem eqItem && eqItem.equipmentType == targetItem.activeEquipSlot.equipType)
                {
                    targetItem.activeEquipSlot.SetItem(carriedItem);
                }
                else
                {
                    return;
                }
            }
        }

        if (carriedItem.activeEquipSlot != null)
        {
            carriedItem.activeEquipSlot.Clear();
        }
        else if (carriedItem.activeSlot != null)
        {
            carriedItem.activeSlot.myItem = null;
        }

        carriedItem = targetItem;
        carriedItem.canvasGroup.blocksRaycasts = false;
        carriedItem.transform.SetParent(draggablesTransform);
    }



}
