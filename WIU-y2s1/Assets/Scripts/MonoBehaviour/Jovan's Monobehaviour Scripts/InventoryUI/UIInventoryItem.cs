using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Inventory.UI
{
    /// <summary>
    /// This class represents a single inventory item in the UI.
    /// It will handle user interactions such as clicking, dragging,dropping
    /// </summary>
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image itemImage; // Image icon
        [SerializeField] private TMP_Text quantityText; // quantiy text
        [SerializeField] private Image borderImage; // border to show when selected

        // Events based on user interactions:
        // Left click to select slot.
        // Right click for item actions.
        // Begin drag to start dragging the item.
        // End drag to stop dragging the item.
        // Drop to drop the item on another slot.
        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

        private bool empty = true; // Check if item slot is empty

        private void Awake()
        {
            ResetData(); // Initialize the item slot as empty
            Deselect(); // Ensure the item is not selected by default
        }

        // Reset the item slot to its initial state
        public void ResetData()
        {
            if (itemImage != null)
                itemImage.gameObject.SetActive(false); // Hide the item image
            empty = true; // Mark the item slot as empty
        }

        // Deselect the item slot by hiding the border
        public void Deselect()
        {
            if (borderImage != null)
                borderImage.enabled = false; // Hide the border image
        }

        // Fills the item slot with data, shows the item image and quantity text.
        public void SetData(Sprite sprite, int quantity)
        {
            if (itemImage != null)
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = sprite;
            }

            if (quantityText != null)
            {
                quantityText.text = quantity.ToString();
            }

            empty = false;
        }

        // Select the item slot by showing the border
        public void Select()
        {
            if (borderImage != null)
                borderImage.enabled = true;
        }

        // Handle pointer click events for the item slot
        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty) return;
            OnItemBeginDrag?.Invoke(this); //null checker
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }
}

// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.