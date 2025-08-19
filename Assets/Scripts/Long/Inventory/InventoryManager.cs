using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]private GameObject itemCursor;

    [SerializeField]private ItemBase itemToAdd;
    [SerializeField]private ItemBase itemToRemove;
    [SerializeField]private GameObject slotHolder;
    [SerializeField]private GameObject hotbarSlotHolder;

    [SerializeField]private InventoryItem[] startingItem;
    [SerializeField]private InventoryItem[] items;

    private GameObject[] slots;
    private GameObject[] hotbarSlots;

    private InventoryItem movingSlot;
    private InventoryItem tempSlot;
    private InventoryItem originalSlot;
    bool isMovingItem = false;
    //private bool isMouseDown = false;

    public ItemBase selectedItem;
    [SerializeField] private GameObject hotbarSelector;
    [SerializeField] private int selectedSlotIndex = 0;
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
        hotbarSlots = new GameObject[hotbarSlotHolder.transform.childCount];

        for(int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i] = hotbarSlotHolder.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new InventoryItem();
        }
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < startingItem.Length; i++)
        {
            AddItemToInventory(startingItem[i].GetItem(), startingItem[i].GetQuantity());
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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (!isMovingItem)
        //    {
        //        if (BeginItemMove())
        //        {
        //            isMouseDown = true;
        //        }
        //    }
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (isMovingItem && isMouseDown)
        //    {
        //        EndItemMove();
        //        isMouseDown = false;
        //    }
        //}
        
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
        else if (Input.GetMouseButtonDown(1))
        {
            if (isMovingItem)
            {
                EndItemMove_Single();
            }
            else
            {
                BeginItemMove_Half();
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex - 1, 0, hotbarSlots.Length - 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            selectedSlotIndex = Mathf.Clamp(selectedSlotIndex + 1, 0, hotbarSlots.Length - 1);
        }

        hotbarSelector.transform.position = hotbarSlots[selectedSlotIndex].transform.position;
        selectedItem = items[selectedSlotIndex + slots.Length - hotbarSlots.Length].GetItem();

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseSelectedItem(gameObject);
        }
    }
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
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
            
        RefreshHotbar();
    }
    public void RefreshHotbar()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            try
            {
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i + slots.Length - hotbarSlots.Length].GetItem().itemIcon;
                if (items[i + slots.Length - hotbarSlots.Length].GetItem().isStackable)
                {
                    hotbarSlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = items[i + slots.Length - hotbarSlots.Length].GetQuantity().ToString();
                }
                else
                {
                    hotbarSlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
            }
            catch
            {
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                hotbarSlots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
            }
        }
            
    }

    public bool AddItemToInventory(ItemBase item, int quantity)
    {
        InventoryItem inventoryItem = Contains(item);
        if (inventoryItem != null)
        {
            var quantityCanAdd = inventoryItem.GetItem().stackSize - inventoryItem.GetQuantity();
            var quantityToAdd = Mathf.Clamp(quantity, 0 , quantityCanAdd);
            var remainder = quantity - quantityToAdd;
            inventoryItem.AddQuantity(quantity);
            if (remainder > 0)
            {
                AddItemToInventory(item, remainder);
            }
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)
                {
                    var quantityCanAdd = item.stackSize - items[i].GetQuantity();
                    var quantityToAdd = Mathf.Clamp(quantity, 0, quantityCanAdd);
                    var remainder = quantity - quantityToAdd;
                    items[i].AddItem(item, quantityToAdd);
                    if (remainder > 0)
                    {
                        AddItemToInventory(item, remainder);
                    }
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
            if (items[i].GetItem() == item && items[i].GetItem().isStackable && items[i].GetQuantity() < items[i].GetItem().stackSize)
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
    private bool BeginItemMove_Half()
    {
        originalSlot = GetClosetSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
        {
            return false;
        }
        movingSlot = new InventoryItem(originalSlot.GetItem(),Mathf.CeilToInt(originalSlot.GetQuantity() / 2f));
        originalSlot.SubtractQuantity(Mathf.CeilToInt(originalSlot.GetQuantity() / 2f));
        if (originalSlot.GetQuantity() < 1)
        {
            originalSlot.Clear();
        }
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
                    if (originalSlot.GetItem().isStackable && originalSlot.GetQuantity() < originalSlot.GetItem().stackSize)
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
                    return true;
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
    private bool EndItemMove_Single()
    {
        originalSlot = GetClosetSlot();
        if (originalSlot == null)
        {
            return false;
        }
        if(originalSlot.GetItem() != null && originalSlot.GetItem() != movingSlot.GetItem())
        {
            return false;
        }
        if (originalSlot.GetItem() != null && originalSlot.GetItem() == movingSlot.GetItem() && !movingSlot.GetItem().isStackable)
        {
            return false;
        }
        else if (originalSlot.GetItem() != null && originalSlot.GetItem() == movingSlot.GetItem())
        {
            movingSlot.SubtractQuantity(1);
            originalSlot.AddQuantity(1);
        }
        else
        {
            movingSlot.SubtractQuantity(1);
            originalSlot.AddItem(movingSlot.GetItem(), 1);
        }

        if (movingSlot.GetQuantity() < 1)
        {
            isMovingItem = false;
            movingSlot.Clear(); // Clear the moving slot if quantity is less than 1
        }
        else
        {
            isMovingItem = true;
        }
        RefreshUI(); 
        return true;
    }
    public void UseSelectedItem(GameObject targetObject)
    {
        if (selectedItem != null)
        {
            selectedItem.Use(targetObject);
            items[selectedSlotIndex + slots.Length - hotbarSlots.Length].SubtractQuantity(1);
            RefreshUI();
        }
    }
    public bool isFull()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == null)
            {
                return false;
            }
        }
        return true;
    }
}
