using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Inventory.Model
{
    /// <summary>
    /// Abstract base class for all inventory items in the game.
    /// Uses ScriptableObjects to define reusable item data
    /// without creating multiple copies in memory.
    /// </summary>

    public abstract class ItemSO : ScriptableObject
    {

        // Will determine if the item would be stackable in the inventory.
        // Example: Coins or Potions (true), Weapons or Armor (false).
        [field: SerializeField]
        public bool IsStackable { get; set; }

        [field: SerializeField] 
        public string ItemKey { get; private set; } = "";

        // The maximum stack size for the item type in the inventory slot.
        // Default is 1, can be changed in the inspector. (e.g. 10 for potions, 1 for weapons)
        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        // Displays the name of the item in the inventory UI.
        // Example: "Health Potion", "Sword".
        [field: SerializeField]
        public string Name { get; set; }

        // Displays the description of the item in the inventory UI.
        // It is able to support multiple lines of text in the inspector.
        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        // The image of the item that will be displayed in the inventory UI.
        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public List<ItemParameter> DefaultParameterList { get; set; }

        public string ActionName;
        [SerializeField] public AudioClip actionSFX { get; private set; }

        public abstract bool PerformAction(GameObject character);
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}

// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.