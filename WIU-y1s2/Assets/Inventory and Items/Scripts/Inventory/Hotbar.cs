using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hotbar", menuName = "Inventory/Hotbar")]
public class Hotbar : ScriptableObject
{
    // For 5 hotbar slots!
    public List<ItemInstance> hotbarSlots = new List<ItemInstance>() { null, null, null, null, null };

    public void AssignItemToSlot(ItemInstance item, int slot)
    {
        if (slot >= 0 && slot < hotbarSlots.Count)
            hotbarSlots[slot] = item;
    }

    public void UseSlot(int slot, GameObject user)
    {
        if (slot < 0 || slot >= hotbarSlots.Count) return;
        var item = hotbarSlots[slot];
        if (item != null)
        {
            item.Use(user);
        }
    }
}