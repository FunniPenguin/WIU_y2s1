using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotbarSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image icon;
    public ItemInstance currentItem;

    public void SetItem(ItemInstance item)
    {
        currentItem = item;
        if (icon == null) return;

        if (item != null && item.itemData != null && item.itemData.animationFrames.Length > 0)
        {
            icon.sprite = item.itemData.animationFrames[0];
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        // Only accept InventorySlotUI drops
        InventorySlotUI draggedSlot = eventData.pointerDrag.GetComponent<InventorySlotUI>();
        if (draggedSlot != null && draggedSlot.GetItem() != null)
        {
            SetItem(draggedSlot.GetItem());

            // Remove from inventory slot
            draggedSlot.SetItem(null);
        }
    }
}
