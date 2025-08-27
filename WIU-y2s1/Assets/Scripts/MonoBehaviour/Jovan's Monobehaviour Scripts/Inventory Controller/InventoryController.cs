using _Inventory.Model;
using _Inventory.UI;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Inventory
{
    /// <summary>
    /// Handles the connection between the inventory data (InventorySO)
    /// and the UI (UIInventory). Responsible for initializing, updating,
    /// and responding to player input for inventory management.
    /// </summary>

    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventory inventoryUI; // It will refer to the UI component displaying inventory slots

        [SerializeField]
        private InventorySO inventoryData; // ScriptableObject that holds the inventory data, including items and their quantities.

        public List<InventoryItem> initialItems = new List<InventoryItem>(); // Optional items to pre-load at start: use it for giving player starting items like health potions, and base weapons.

        [SerializeField] private AudioSource audioSource; // Audio source for playing item action sound effects

        [SerializeField] private AudioClip dropClip; // Sound effect for dropping an item

        private void Start()
        {
            PrepareUI(); //set up the UI for the inventory
            PrepareInventoryData(); //initialize the inventory data and load initial items
        }


        // Initializes the inventory data, subscribes to updates,
        // and adds any predefined starting items.
        private void PrepareInventoryData()
        {
            inventoryData.Initialize(); // Reset/initialize inventory
            inventoryData.OnInventoryUpdated += UpdateInventoryUI; // Subscribe to inventory updates to refresh the UI when items change

            // Add starting items (if any)
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;

                inventoryData.AddItem(item);
            }
        }

        // Updates the UI whenever the inventory data changes.
        // Resets and redraws all slots to reflect current state.
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                // Slot index is the key, itemImage is the sprite for the item, and quantity is how many of that item are in the slot (stack size basically)
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }   
        }

        // Prepares the UI by setting up its size and event subscriptions.
        private void PrepareUI()
        {
            // Match the UI slots to the inventory data size
            inventoryUI.InitializeInventoryUI(inventoryData.Size);

            // Subscribe to UI events to handle user interactions
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        // Handles user input for item selection, dragging, and actions.
        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            // Check if the item has an action like consume or equip
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }
            // If the item is destroyable, remove it from the inventory
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Discard", () => DropItem(itemIndex, inventoryItem.quantity));
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            // If the item is destroyable, remove it from the inventory
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            // Check if the item has an action like consume or equip
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                {
                    inventoryUI.ResetSelection();
                }
            }
        }




        // Called when the player starts dragging an item, will display the item being dragged, with the alpha being lowwered
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        // Will handle the swapping of items in the inventory between 2 inventory slots.
        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        // Displays the item's description when the player selects an item.
        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);

            // If the item is empty, reset the description UI and return
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            // Update the description UI with the item's details
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);

        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} "
                    + $": +{inventoryItem.itemState[i].value}"
                    //+ $" / {inventoryItem.item.DefaultParameterList[i].value}"
                    );
                sb.AppendLine();
            }
            return sb.ToString();
        }


        // Handles the player's input for togglin the Inventory UI.
        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    // Show inventory and sync current data
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    // Hide inventory
                    //inventoryUI.Hide();
                }
            }
        }
    }
}

// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.