using _Inventory.Model;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IDataPersistence
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; } // The item data, which includes properties like name, description, image, etc.

    [field: SerializeField]
    public int Quantity { get; set; } = 1; // The quantity of the item, default is 1. This is used for stackable items.

    [SerializeField]
    private AudioSource audioSource; // Audio source for playing sound effects when the item is picked up

    [SerializeField]
    private float duration = 0.3f; // Duration of the pickup animation, default is 0.3 seconds

    [SerializeField] private string GUID = "";

    private void Awake()
    {
        if (GUID == "")
        {
            GUID = DataPersistenceManager.GenerateGUID();
        }
    }
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage; // Set the sprite of the item based on the ItemSO data
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further interactions
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        audioSource.Play(); // Play the pickup sound effect
        Vector3 startScale = transform.localScale; // Store the initial scale of the item
        Vector3 endScale = Vector3.zero; // Define the end scale as zero, which will make the item disappear
        float currentTime = 0; // Initialize the current time for the animation

        // Animate the item from its current scale to zero over the specified duration
        while (currentTime < duration) 
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration); // Interpolate the scale
            yield return null;
        } 

        gameObject.SetActive(false);
    }
    public void SaveData(GameData data)
    {
        if (data.mapGameObjects.ContainsKey(GUID))
        {
            data.mapGameObjects.Remove(GUID);
        }
        data.mapGameObjects.Add(GUID, gameObject.activeInHierarchy);
    }
    public void LoadData(GameData data) {
        bool isActive = true;
        if (data.mapGameObjects.TryGetValue(GUID, out isActive))
            gameObject.SetActive(isActive);
        else
            gameObject.SetActive(true);
    }
}
// Made by Jovan Yeo Kaisheng