using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]private GameObject itemCursor;

    [SerializeField]private ItemBase itemToAdd;
    [SerializeField]private ItemBase itemToRemove;
    [SerializeField]private GameObject slotHolder;

    [SerializeField]private InventoryItem[] startingItem;
    [SerializeField]private InventoryItem[] items;

    private GameObject[] slots;

    private InventoryItem movingSlot;
    private InventoryItem tempSlot;
    private InventoryItem originalSlot;
    bool isMovingItem = false;
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

    void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        items = new InventoryItem[slots.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new InventoryItem();
        }
        for (int i = 0; i < startingItem.Length; i++)
        {
            items[i] = startingItem[i];
        }

        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        RefreshUI();
        AddItemToInventory(itemToAdd,1);
        RemoveItemFromInventory(itemToRemove);
    }
    private void Update()
    {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;
        if (isMovingItem)
        {
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isMovingItem)
            {
                EndItemMove();
            }
            else
            {
                BeginItemMove();
            }
        }
    }
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                if (items[i].GetItem().isStackable)
                {
                    slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = items[i].GetQuantity().ToString();
                }
                else
                {
                    slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
            }
    }

    public bool AddItemToInventory(ItemBase item, int quantity)
    {
        InventoryItem inventoryItem = Contains(item);
        if (inventoryItem != null && inventoryItem.GetItem().isStackable)
        {
            inventoryItem.AddQuantity(1);
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)
                {
                    items[i].AddItem(item, quantity);
                    break;
                }
            }
        }
        RefreshUI();
        return true;
    }
    public bool RemoveItemFromInventory(ItemBase item)
    {
        InventoryItem temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
            {
                temp.SubtractQuantity(1);
            }
            else
            {
                int itemToRemoveIndex = -1;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        itemToRemoveIndex = i;
                        break;
                    }
                }
                items[itemToRemoveIndex].Clear();
            }
        }
        else
        {
            return false; // Item not found in inventory
        }
        RefreshUI();
        return true; // Item successfully removed
    }
    public InventoryItem Contains(ItemBase item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item)
            {
                return items[i];
            }
        }
        return null;
    }

    private InventoryItem GetClosetSlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 40)
            {
                return items[i];
            }
        }
        return null;
    }
    private bool BeginItemMove()
    {
        originalSlot = GetClosetSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
        {
            return false;
        }
        movingSlot = new InventoryItem(originalSlot);
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private bool EndItemMove()
    {
        originalSlot = GetClosetSlot();
        if (originalSlot == null)
        {
            AddItemToInventory(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        else
        {
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem())//same item
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.Clear();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    tempSlot = new InventoryItem(originalSlot); // a=b
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());//b=c
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity());//c=a
                    RefreshUI();
                    movingSlot.Clear();
                }
            }
            else
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.Clear();
            }
        }
        isMovingItem = false;
        RefreshUI();
        return true;
    }
}
