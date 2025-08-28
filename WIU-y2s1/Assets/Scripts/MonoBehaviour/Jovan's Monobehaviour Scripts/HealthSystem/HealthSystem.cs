using UnityEngine;

public class HealthSystem : MonoBehaviour, IDataPersistence
{
    [Header("Health Data")]
    public Health healthData; // sore health data as a ScriptableObject
    
    [Header("UI")]
    [SerializeField] private FloatingHealthBar healthBar; // reference to the health bar UI component

    public bool isInvincible = false; // flag to check if player is invincible

    private void Awake()
    {
        if (healthData == null)
        {
            Debug.LogError("HealthData not assigned!");
            return;
        }

        healthData.currentHealth = healthData.maxHealth; // initialize current health to max health

        if (healthBar == null)
            healthBar = GetComponentInChildren<FloatingHealthBar>(); // try to find FloatingHealthBar in children

        UpdateHealthUI(); 
    }

    public void Hurt(float damage)
    {
        if (isInvincible) return; // if player is invincible, do not take damage

        healthData.currentHealth -= damage; // reduce current health by damage amount
        healthData.currentHealth = Mathf.Clamp(healthData.currentHealth, 0, healthData.maxHealth); // ensure current health does not go below 0 or above max health
        UpdateHealthUI();

        if (healthData.currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        healthData.currentHealth += amount; // increase current health by heal amount
        if (healthData.currentHealth > healthData.maxHealth)
            healthData.currentHealth = healthData.maxHealth; // ensure current health does not exceed max health
        UpdateHealthUI();
    }

    public void RestoreFullHealth()
    {
        healthData.currentHealth = healthData.maxHealth; // restore current health to max health
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.UpdateHealthBar(healthData.currentHealth, healthData.maxHealth); // update the health bar UI with current and max health
    }
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        gameObject.GetComponent<EntityStatistics>().uponDeath.Invoke(); // invoke death event from EntityStatistics
    }

    public void SaveData(GameData data)
    {
        data._health = healthData.currentHealth;
    }
    public void LoadData(GameData data)
    {
        healthData.currentHealth = data._health;
        UpdateHealthUI();
    }
}
// Made by Jovan Yeo Kaisheng
// This is part of the Health System in the game