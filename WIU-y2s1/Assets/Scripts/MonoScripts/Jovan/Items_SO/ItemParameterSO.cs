using UnityEngine;

namespace _Inventory.Model
{
    [CreateAssetMenu(fileName = "ItemParameterSO", menuName = "Inventory/ItemParameterSO", order = 0)]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField] 
        public string ParameterName { get; private set; }

        [field: SerializeField]
        public string ParameterKey { get; private set; }
    }
}

// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.
