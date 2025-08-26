using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Inventory.UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab; // Prefab for each inventory item UI

        [SerializeField]
        private RectTransform contentPanel; // Panel that holds all inventory items

        [SerializeField]
        private UIInventoryDescription itemDescription; // UI component for displaying item descriptions

        [SerializeField] private MouseFollower mouseFollower; // Follower for the item being dragged

        List<UIInventoryItem> ListofUIItems = new List<UIInventoryItem>(); // List to hold all UI inventory items

        private int currentlyDraggedItemIndex = -1; // Index of the currently dragged item, -1 if none is being dragged

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging; // Event for when an item description is requested, item action is requested, or dragging starts
        public event Action<int, int> OnSwapItems; // Event for when two items are swapped in the inventory

        [SerializeField] private ItemActionPanel actionPanel; // Panel for item actions

        private void Awake()
        {
            //Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        // setting up the inventory UI with a specified size
        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity); // Create a new UIInventoryItem instance
                uiItem.transform.SetParent(contentPanel); // Set its parent to the content panel
                ListofUIItems.Add(uiItem); // Add it to the list of UI items

                uiItem.OnItemClicked += HandleItemSelection; // Subscribe to item click event to handle selection
                uiItem.OnItemBeginDrag += HandleBeginDrag; // Subscribe to item begin drag event to handle dragging
                uiItem.OnItemDroppedOn += HandleSwap; // Subscribe to item drop event to handle swapping items
                uiItem.OnItemEndDrag += HandleEndDrag; // Subscribe to item end drag event to reset the dragged item
                uiItem.OnRightMouseBtnClick += HandleShowItemActions; // Subscribe to right mouse button click event to use item
            }
        }

        // update the UI for a specific item index with new data
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (ListofUIItems.Count > itemIndex)
            {
                ListofUIItems[itemIndex].SetData(itemImage, itemQuantity); // Set the data for the UI item at the specified index
            }
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = ListofUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = ListofUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = ListofUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }
        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = ListofUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnDescriptionRequested?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex) 
        { 
            actionPanel.Toggle(true);
            actionPanel.transform.position = ListofUIItems[itemIndex].transform.position;
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in ListofUIItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }

        //public void Hide()
        //{
        //    actionPanel.Toggle(false);
        //    gameObject.SetActive(false);
        //    ResetDraggedItem();
        //}

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            ListofUIItems[itemIndex].Select();
        }

        internal void ResetAllItems()
        {
            foreach(var item in ListofUIItems){
                item.ResetData();
                item.Deselect();
            }
        }
    }
}
// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.