using System.Collections.Generic;
using _Inventory.Model;

[System.Serializable]
public class SerializableInventoryItem
{
    public string itemKey; //key from ItemSO.ItemKey
    public int quantity;
    public List<SerializableItemParameter> itemState;
    public int slotIndex; // Restore to exact slot
}

// Made by Jovan Yeo Kaisheng
