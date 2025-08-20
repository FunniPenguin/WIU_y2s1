using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public PlayerInventoryComponent playerInventory;
    public InventorySlotUI[] inventorySlots;

    private void OnEnable()
    {
        playerInventory.inventory.OnInventoryChanged += UpdateSlots;
        UpdateSlots();
    }

    private void OnDisable()
    {
        playerInventory.inventory.OnInventoryChanged -= UpdateSlots;
    }

    private void UpdateSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < playerInventory.inventory.items.Count)
                inventorySlots[i].SetItem(playerInventory.inventory.items[i]);
            else
                inventorySlots[i].SetItem(null);
        }
    }
}
