using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventoryComponent playerInventory = collision.GetComponent<PlayerInventoryComponent>();
        if (playerInventory != null)
        {
            playerInventory.inventory.AddItem(itemData);
            Debug.Log($"Picked up {itemData.itemName}");
            gameObject.SetActive(false);
        }
    }
}
