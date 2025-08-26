using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthbar; // Reference to the Slider component for the health bar

    private void Awake()
    {
        if (healthbar == null)
            healthbar = GetComponentInChildren<Slider>(); // Try to find the Slider component in children

        if (healthbar == null)
            Debug.LogError("No Slider found for FloatingHealthBar!"); // error if no Slider is found
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        if (healthbar != null)
        {
            healthbar.value = currentValue / maxValue; // Update the health bar value based on current and max health
        }
    }
}    
// Made by Jovan Yeo Kaisheng
// This code is part of the Health System.
