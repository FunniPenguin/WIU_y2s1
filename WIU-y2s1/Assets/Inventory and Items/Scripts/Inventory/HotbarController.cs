using UnityEngine;

public class HotbarController : MonoBehaviour
{
    public Hotbar hotbar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) hotbar.UseSlot(0, gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha2)) hotbar.UseSlot(1, gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha3)) hotbar.UseSlot(2, gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha4)) hotbar.UseSlot(3, gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha5)) hotbar.UseSlot(4, gameObject);
    }
}