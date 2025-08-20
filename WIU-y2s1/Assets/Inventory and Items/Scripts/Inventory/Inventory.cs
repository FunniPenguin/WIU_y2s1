using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player's Inventory", menuName = "Inventory/Player Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemInstance> items = new List<ItemInstance>();

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    public void AddItem(ItemData data)
    {
        foreach (var item in items)
        {
            if (item.itemData == data)
            {
                OnInventoryChanged?.Invoke();
                return;
            }
        }
        items.Add(new ItemInstance(data));
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemInstance instance)
    {
        if (items.Contains(instance))
        {
            items.Remove(instance);
            OnInventoryChanged?.Invoke();
        }
    }
}