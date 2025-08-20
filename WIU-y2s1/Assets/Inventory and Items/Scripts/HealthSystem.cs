using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Data")]
    public Health healthData;

    [Header("UI")]
    [SerializeField] private FloatingHealthBar healthBar;

    private float currentHealth;

    public bool isInvincible = false;

    private void Awake()
    {
        if (healthData == null)
        {
            Debug.LogError("HealthData not assigned!");
            return;
        }

        currentHealth = healthData.maxHealth;

        if (healthBar == null)
            healthBar = GetComponentInChildren<FloatingHealthBar>();

        UpdateHealthUI();
    }

    public void Hurt(float damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, healthData.maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > healthData.maxHealth)
            currentHealth = healthData.maxHealth;
        UpdateHealthUI();
    }

    public void RestoreFullHealth()
    {
        currentHealth = healthData.maxHealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.UpdateHealthBar(currentHealth, healthData.maxHealth);
    }
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);
    }
}
