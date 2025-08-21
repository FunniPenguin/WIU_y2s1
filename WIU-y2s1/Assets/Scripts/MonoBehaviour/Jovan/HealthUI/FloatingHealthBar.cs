using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthbar;

    private void Awake()
    {
        if (healthbar == null)
            healthbar = GetComponentInChildren<Slider>();

        if (healthbar == null)
            Debug.LogError("No Slider found for FloatingHealthBar!");
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        if (healthbar != null)
        {
            healthbar.value = currentValue / maxValue;
            Debug.Log("Updating Health Bar: " + healthbar.value);
        }
    }
}
