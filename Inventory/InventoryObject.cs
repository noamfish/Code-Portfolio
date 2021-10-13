using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _quantity)
    {
        bool hasItem = false;

        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].item == _item)
            {
                inventorySlots[i].IncreaseQuantity(_quantity);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            inventorySlots.Add(new InventorySlot(_item, _quantity));
        }
    }


}
[System.Serializable]
public class InventorySlot
{
    public int quantity;
    public ItemObject item;

    public InventorySlot(ItemObject _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
    }
    public void IncreaseQuantity(int _quantity)
    {
        quantity += _quantity;
    }
}
