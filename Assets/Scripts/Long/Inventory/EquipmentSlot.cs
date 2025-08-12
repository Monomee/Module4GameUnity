using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public EquipmentType slotType;
    public EquipmentItem equippedItem;

    public void Equip(EquipmentItem item, GameObject player)
    {
        if (item.equipmentType != slotType) return;
        if (equippedItem != null)
            equippedItem.Unequip(player);
        equippedItem = item;
        equippedItem.Equip(player);
        //Update UI or any other necessary components here
    }
}
