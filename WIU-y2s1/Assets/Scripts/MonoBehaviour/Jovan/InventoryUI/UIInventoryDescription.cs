using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Inventory.UI
{
    /// <summary>
    ///  Controls the display of item descriptions in the inventory UI.
    ///  Shows the item's image, name, and description when an item is selected.
    /// </summary>
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage; // Image component to display the item sprite
        [SerializeField] private TMP_Text title; // Text component for the item name
        [SerializeField] private TMP_Text description; // Text component for the item description

        public void Awake()
        {
            ResetDescription(); // Initialize the description UI to be empty
        }

        // Clears the description UI elements
        public void ResetDescription()
        {
            itemImage.gameObject.SetActive(false); // Hide the item image
            title.text = ""; // Clear the item name text
            description.text = ""; // Clear the item description text
        }

        // Sets the description UI with the provided item data
        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            itemImage.gameObject.SetActive(true); // Show the item image
            itemImage.sprite = sprite; // Set the item image sprite
            title.text = itemName; // Set the item name text
            description.text = itemDescription; // Set the item description text
        }
    }
}

// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.