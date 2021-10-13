using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryObject inventory;
    public InventorySlotVisuals inventorySlotPrefab;
    public GameObject gridLayoutParent;
    private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    public void UpdateVisuals(ItemObject _item)
    {
        for(int i = 0; i < inventory.inventorySlots.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.inventorySlots[i]))
            {
                itemsDisplayed[inventory.inventorySlots[i]].GetComponentInChildren<Text>().text = inventory.inventorySlots[i].quantity.ToString();
            }
            else
            {
                GameObject newSlot = Instantiate(inventory.inventorySlots[i].item.slotVisuals, gridLayoutParent.transform);
                newSlot.GetComponent<InventorySlotVisuals>().icon.sprite = inventory.inventorySlots[i].item.iconImage;
                newSlot.GetComponent<InventorySlotVisuals>().text.text = inventory.inventorySlots[i].quantity.ToString();
                itemsDisplayed.Add(inventory.inventorySlots[i], newSlot);
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.inventorySlots.Clear();
    }
}
