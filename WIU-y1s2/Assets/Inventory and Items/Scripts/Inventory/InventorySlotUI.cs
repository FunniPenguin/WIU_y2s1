using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image icon;
    public ItemInstance currentItem { get; private set; }

    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private RectTransform parentPanel;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        parentPanel = transform.parent.GetComponent<RectTransform>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

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

    public ItemInstance GetItem() => currentItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;
        originalParent = transform.parent;
        transform.SetParent(canvas.transform, true); // move to top of UI
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentItem == null) return;

        Vector3 newPos = eventData.position;

        Vector3[] corners = new Vector3[4];
        parentPanel.GetWorldCorners(corners);
        newPos.x = Mathf.Clamp(newPos.x, corners[0].x, corners[2].x);
        newPos.y = Mathf.Clamp(newPos.y, corners[0].y, corners[2].y);

        rectTransform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent, true);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Swap with another inventory slot
        InventorySlotUI draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        if (draggedSlot != null && draggedSlot != this)
        {
            ItemInstance temp = currentItem;
            SetItem(draggedSlot.GetItem());
            draggedSlot.SetItem(temp);
            return;
        }

        // Drop onto hotbar
        HotbarSlotUI hotbarSlot = eventData.pointerDrag?.GetComponent<HotbarSlotUI>();
        if (hotbarSlot != null && currentItem != null)
        {
            hotbarSlot.SetItem(currentItem);
            SetItem(null);
        }
    }
}
