// New: SerializableInventoryItem.cs
using System.Collections.Generic;
using _Inventory.Model;

[System.Serializable]
public class SerializableInventoryItem
{
    public string itemKey; // Stable key from ItemSO.ItemKey
    public int quantity;
    public List<ItemParameter> itemState; // Already serializable
    public int slotIndex; // Restore to exact slot
}
