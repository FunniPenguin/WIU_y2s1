using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems; // List of items in the inventory, each represented by an InventoryItem struct

        [field: SerializeField]
        public int Size { get; private set; } = 10; // The maximum number of items the inventory can hold, default is 10

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; // Event that is triggered whenever the inventory is updated, passing the current state of the inventory

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>(); // Initialize the inventory items list
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem()); // Fill the inventory with empty items up to the specified size
            }
        }

        public int AddItem(ItemSO item, int quantity)
        {
            // Check if the item is null or quantity is less than or equal to zero, and also check if the item is not stackable
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1); // Add the item to the first free slot in the inventory aka the next empty slot
                    }
                }
                InformAboutChange(); // Notify listeners that the inventory has been updated
                return quantity; // Return the remaining quantity that could not be added
            }
            quantity = AddStackableItem(item,quantity); // If the item is stackable, try to add it to existing stacks or create new stacks in the inventory
            InformAboutChange(); // Notify listeners that the inventory has been updated
            return quantity; // Return the remaining quantity that could not be added
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            // Define a new InventoryItem with the provided item and quantity
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity
            };

            // Iterate through the inventory items to find the first empty slot
            for (int i = 0; i < inventoryItems.Count;i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem; // Place the new item in the first empty slot
                    return quantity; // Return the quantity that was added to the inventory
                }
            }
            return 0; // If no empty slot is found, return 0 indicating that the item could not be added
        }

        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false; // Check if the inventory is full by checking if there are no empty slots left


        // Adds a stackable item to the inventory, either by increasing the quantity of an existing stack or creating a new stack if necessary
        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i =0; i < inventoryItems.Count; i++)
            {
                // Skip empty slots in the inventory
                if (inventoryItems[i].IsEmpty)
                    continue;
                // If the item in the current slot matches the item being added
                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity; // Calculate how much more of this item can be added to the current stack

                    // If the current stack can accommodate more of the item
                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize); // Fill the stack to its maximum size
                        quantity -= amountPossibleToTake; // Decrease the quantity by the amount added to the stack
                    } 
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity); // If the current stack can accommodate the entire quantity, just increase the stack size
                        InformAboutChange(); // Notify listeners that the inventory has been updated
                        return 0; // Return 0 as all of the quantity has been added to the stack
                    }
                }
            }
            // If no existing stack was found, add the item to the first free slot
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize); // Ensure that the quantity does not exceed the maximum stack size
                quantity -= newQuantity; // Decrease the quantity by the amount added to the stack
                AddItemToFirstFreeSlot(item, newQuantity); // Add the item to the first free slot in the inventory
            }
            return quantity; // Return the remaining quantity that could not be added to the inventory
        }

        // Retrieves the current state of the inventory, returning a dictionary where the key is the item index and the value is the InventoryItem at that index
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>(); // Create a new dictionary to hold the current inventory state

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                // Skip empty slots in the inventory
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i]; // Add the item at index i to the dictionary, using the index as the key
            }
            return returnValue; // Return the dictionary containing the current inventory state
        }

        // Resets all items in the inventory to empty, effectively clearing the inventory
        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex]; // Returns the InventoryItem at the specified index, which can be empty or contain an item
        }

        // Adds an item to the inventory using an InventoryItem struct, which contains both the item and its quantity
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity); // Add the item to the inventory using the item's data and quantity
        }

        // Handles the request to show the description of an item at a specific index
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = inventoryItems[itemIndex_1]; // Store the item at index 1 temporarily
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2]; // Swap the item at index 1 with the item at index 2
            inventoryItems[itemIndex_2] = item1; // Place the temporarily stored item at index 2
            InformAboutChange(); // Notify listeners that the inventory has been updated
        }

        // Notifies listeners about changes in the inventory state, triggering the OnInventoryUpdated event
        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState()); // Invoke the OnInventoryUpdated event with the current inventory state
        }

        // Removes a specified amount of an item from the inventory at a given index
        public void RemoveItem(int itemIndex, int amount)
        {
            // Check if the item index is valid and if the item at that index is not empty
            if (inventoryItems.Count > itemIndex)
            {
                // If the item at the specified index is empty, do nothing
                if (inventoryItems[itemIndex].IsEmpty)
                    return;

                // If the amount to remove is greater than the quantity of the item, set the item to empty
                int reminder = inventoryItems[itemIndex].quantity - amount;

                // If the reminder is less than or equal to zero, remove the item from the inventory
                if (reminder <= 0)
                {
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem(); // Set the item at the specified index to an empty item
                }
                else
                {
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder); // Otherwise, change the quantity of the item at the specified index to the reminder amount
                }   
                InformAboutChange(); // Notify listeners that the inventory has been updated
            }
        }   
    }

    // Represents an item in the inventory, containing the item data and its quantity.

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null; // Check if the item is empty by checking if the item is null

        // Changes the quantity of the item, returning a new InventoryItem with the updated quantity
        public InventoryItem ChangeQuantity(int newQuantity)
        {
            // Ensure that the new quantity is not negative
            return new InventoryItem 
            {
                item = this.item, // Keep the same item
                quantity = newQuantity, // Set the new quantity
            };
        }

        // Creates an empty InventoryItem, which has no item and a quantity of zero
        public static InventoryItem GetEmptyItem() 
            => new InventoryItem
            {
                item = null, // No item assigned
                quantity = 0, // Quantity is zero
            };
    }
}


// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.