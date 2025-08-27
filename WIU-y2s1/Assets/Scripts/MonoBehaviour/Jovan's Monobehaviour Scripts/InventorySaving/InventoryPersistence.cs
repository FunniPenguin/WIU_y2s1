using _Inventory.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPersistence : MonoBehaviour, IDataPersistence
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemSO> itemCatalog = new List<ItemSO>();
    [SerializeField] private List<ItemParameterSO> parameterCatalog = new List<ItemParameterSO>();

    private Dictionary<string, ItemSO> _itemsByKey; // Dictionary will lookup itemKey for ItemSO
    private Dictionary<string, ItemParameterSO> _paramsByKey; // Dictionary will lookup parameterKey for ItemParameterSO



    private void Awake()
    {
        _itemsByKey = itemCatalog // Takes the list of all items (itemCatalog)
            .Where(x => x != null && !string.IsNullOrEmpty(x.ItemKey)) // Ignores any nulls or missing keys.
            .GroupBy(x => x.ItemKey) // Groups by ItemKey (so if two items have the same key, it just picks the first).
            .ToDictionary(g => g.Key, g => g.First()); // Builds _byKey dictionary for instant itemKey to ItemSO lookups later.

        _paramsByKey = parameterCatalog
            .Where(x => x != null && !string.IsNullOrEmpty(x.ParameterKey))
            .GroupBy(x => x.ParameterKey)
            .ToDictionary(g => g.Key, g => g.First());

    }

    public void SaveData(GameData data)
    {
        if (inventoryData == null) return;

        data.inventoryItems.Clear(); // Clears out the old saved inventory in GameData.
        foreach (var kvp in inventoryData.GetCurrentInventoryState()) // kvp.Key = slotIndex
        {
            var invItem = kvp.Value;
            if (invItem.IsEmpty || invItem.item == null) continue; // Loops over every slot that currently has an item in InventorySO.


            // Create a serializable version of the inventory item to store in GameData.
            var s = new SerializableInventoryItem
            {
                itemKey = invItem.item.ItemKey,
                quantity = invItem.quantity,
                itemState = invItem.itemState.Select(p => new SerializableItemParameter {parameterKey = p.itemParameter.ParameterKey,value = p.value}).ToList(),
                slotIndex = kvp.Key
            };
            data.inventoryItems.Add(s);
        }
    }

    public void LoadData(GameData data)
    {
        if (inventoryData == null) return;

        // Ensure inventory is initialized then clear
        if (data == null || data.inventoryItems == null) return;

        inventoryData.Initialize();
        inventoryData.ClearAll();

        foreach (var s in data.inventoryItems.OrderBy(i => i.slotIndex))
        {
            if (string.IsNullOrEmpty(s.itemKey)) continue;
            if (!_itemsByKey.TryGetValue(s.itemKey, out var itemSO) || itemSO == null) continue;

            var inv = new InventoryItem
            {
                item = itemSO,
                quantity = s.quantity,
                itemState = s.itemState != null ? s.itemState.Select(p => new ItemParameter
                {
                    itemParameter = _paramsByKey.TryGetValue(p.parameterKey, out var paramSO) ? paramSO : null,
                    value = p.value
                }).Where(p => p.itemParameter != null).ToList() : new List<ItemParameter>(itemSO.DefaultParameterList)
            };

            // Restore to the exact slot (and bounds-check)
            if (s.slotIndex >= 0 && s.slotIndex < inventoryData.Size)
                inventoryData.SetItemAt(s.slotIndex, inv);
        }
    }
}

// Made by Jovan Yeo Kaisheng