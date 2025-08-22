using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Data")]
    public Health healthData; // sore health data as a ScriptableObject

    [Header("UI")]
    [SerializeField] private FloatingHealthBar healthBar; // reference to the health bar UI component

    private float currentHealth; // current health of player

    public bool isInvincible = false; // flag to check if player is invincible

    private void Awake()
    {
        if (healthData == null)
        {
            Debug.LogError("HealthData not assigned!");
            return;
        }

        currentHealth = healthData.maxHealth; // initialize current health to max health

        if (healthBar == null)
            healthBar = GetComponentInChildren<FloatingHealthBar>(); // try to find FloatingHealthBar in children

        UpdateHealthUI(); 
    }

    public void Hurt(float damage)
    {
        if (isInvincible) return; // if player is invincible, do not take damage

        currentHealth -= damage; // reduce current health by damage amount
        currentHealth = Mathf.Clamp(currentHealth, 0, healthData.maxHealth); // ensure current health does not go below 0 or above max health
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount; // increase current health by heal amount
        if (currentHealth > healthData.maxHealth)
            currentHealth = healthData.maxHealth; // ensure current health does not exceed max health
        UpdateHealthUI();
    }

    public void RestoreFullHealth()
    {
        currentHealth = healthData.maxHealth; // restore current health to max health
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.UpdateHealthBar(currentHealth, healthData.maxHealth); // update the health bar UI with current and max health
    }
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        gameObject.SetActive(false); // deactivate the player object on death
    }
}
// Made by Jovan Yeo Kaisheng
// This is part of the Health System in the game