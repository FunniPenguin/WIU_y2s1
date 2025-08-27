using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO
    {
        [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>(); // List of modifiers that this item will apply when consumed
        private void Awake()
        {
            ActionName = "Use";
        } 

        public override bool PerformAction(GameObject character)
        {
            // Check if the character has a modifiable component
            foreach (ModifierData data in modifiersData)
            {
                data.statModifer.AffectCharacter(character, data.value); // Apply each modifier to the character
            }
            return true;
        }
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
