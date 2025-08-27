using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _Inventory.Model;

public class InventoryPersistence : MonoBehaviour, IDataPersistence
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemSO> itemCatalog = new List<ItemSO>(); // assign all ItemSOs here in Inspector

    private Dictionary<string, ItemSO> _byKey;

    private void Awake()
    {
        _byKey = itemCatalog
            .Where(x => x != null && !string.IsNullOrEmpty(x.ItemKey))
            .GroupBy(x => x.ItemKey)
            .ToDictionary(g => g.Key, g => g.First());
    }

    public void SaveData(GameData data)
    {
        if (inventoryData == null) return;

        data.inventoryItems.Clear();
        foreach (var kvp in inventoryData.GetCurrentInventoryState()) // kvp.Key = slotIndex
        {
            var invItem = kvp.Value;
            if (invItem.IsEmpty || invItem.item == null) continue;

            var s = new SerializableInventoryItem
            {
                itemKey = invItem.item.ItemKey,
                quantity = invItem.quantity,
                itemState = new List<ItemParameter>(invItem.itemState),
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
            if (!_byKey.TryGetValue(s.itemKey, out var itemSO) || itemSO == null) continue;

            var inv = new InventoryItem
            {
                item = itemSO,
                quantity = s.quantity,
                itemState = s.itemState != null
                    ? new List<ItemParameter>(s.itemState)
                    : new List<ItemParameter>(itemSO.DefaultParameterList)
            };

            // Restore to the exact slot (and bounds-check)
            if (s.slotIndex >= 0 && s.slotIndex < inventoryData.Size)
                inventoryData.SetItemAt(s.slotIndex, inv);
        }
    }
}
