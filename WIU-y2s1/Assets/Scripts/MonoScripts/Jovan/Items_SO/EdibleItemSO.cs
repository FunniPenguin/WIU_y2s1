using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>(); // List of modifiers that this item will apply when consumed
        public string ActionName => "Use"; // The name of the action that this item performs, in this case, "Consume"

        [field: SerializeField] 
        public AudioClip actionSFX {get; private set; } // The sound effect that plays when the item is consumed

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            // Check if the character has a modifiable component
            foreach (ModifierData data in modifiersData)
            {
                data.statModifer.AffectCharacter(character, data.value); // Apply each modifier to the character
            }
            return true;
        }
    }

    public interface IDestroyableItem
    {
        // Remove m from inventory when used
    }

    public interface IItemAction
    {
        public string ActionName { get; } // The name of the action that this item performs, e.g., "Consume", "Equip", etc.
        public AudioClip actionSFX { get; } // The sound effect that plays when the item is used
        bool PerformAction(GameObject character, List<ItemParameter> itemState); // Method to perform the action associated with the item, e.g., consuming it, equipping it, etc.
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModiferSO statModifer; // The modifier that will be applied to the character when the item is consumed
        public float value; // The value that will be passed to the modifier when it is applied
    }
}
// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.
