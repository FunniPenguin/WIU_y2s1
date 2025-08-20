using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

    }


}
