using _Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData; // Reference to the inventory data ScriptableObject that holds the inventory state

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>(); // Get the Item component from the collided object
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity); // Attempt to add the item to the inventory, returns the remaining quantity if it exceeds the stack limit
            if (reminder == 0)
                item.DestroyItem(); // If the item was successfully added and no quantity remains, destroy the item object in this case i set it to inactives
            else
                item.Quantity = reminder; // If there is a remainder, update the item's quantity to reflect how much was not added to the inventory
        }
    }
}
// Made by Jovan Yeo Kaisheng