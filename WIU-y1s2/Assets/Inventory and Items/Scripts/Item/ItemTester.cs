using UnityEngine;

public class ItemTester : MonoBehaviour
{
    public ItemData jumpBoostItem;
    public ItemData speedBoostItem;
    public ItemData maxHealthItem;
    public ItemData smallHealthItem;
    public ItemData invincibilityItem;

    private ItemInstance jumpInstance;
    private ItemInstance speedInstance;
    private ItemInstance maxHealthInstance;
    private ItemInstance smallHealthInstance;
    private ItemInstance invincibilityInstance;

    private void Start()
    {
        jumpInstance = new ItemInstance(jumpBoostItem);
        speedInstance = new ItemInstance(speedBoostItem);
        maxHealthInstance = new ItemInstance(maxHealthItem);
        smallHealthInstance = new ItemInstance(smallHealthItem);
        invincibilityInstance = new ItemInstance(invincibilityItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            jumpInstance.Use(gameObject);
            Debug.Log("Jump Boost Used");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            speedInstance.Use(gameObject);
            Debug.Log("Speed Boost used");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            maxHealthInstance.Use(gameObject);
            Debug.Log("Max Health Potion Used!");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            smallHealthInstance.Use(gameObject);
            Debug.Log("Small Health Potion Used!");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            invincibilityInstance.Use(gameObject);
            Debug.Log("Invincibility Potion Used!");
        }
    }
}
