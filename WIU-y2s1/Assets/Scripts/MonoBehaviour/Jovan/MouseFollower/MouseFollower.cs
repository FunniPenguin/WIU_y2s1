using _Inventory.UI;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas; // Reference to the canvas to convert screen points to local points
    [SerializeField] private UIInventoryItem item; // Reference to the UIInventoryItem that will be displayed as a mouse follower

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();// Get the canvas from the root of the transform hierarchy
        item = GetComponentInChildren<UIInventoryItem>(); // Get the UIInventoryItem component from the children of this GameObject
    }
    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity); // Set the data for the UIInventoryItem to display the sprite and quantity
    }
    void Update()
    {
        Vector2 position; // Variable to hold the local position in the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position); // Convert the screen position of the mouse to a local position in the canvas
        transform.position = canvas.transform.TransformPoint(position); // Set the position of this GameObject to the converted local position in the canvas
    }
    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}"); // Log the toggle state of the item
        gameObject.SetActive(val); // Set the active state of this GameObject based on the provided value
    }
}
// Made by Jovan Yeo Kaisheng
