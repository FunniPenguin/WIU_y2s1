using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Data")]
    public Health health;

    [Header("UI")]
    [SerializeField] private FloatingHealthBar healthBar;

    private float currentHealth;

    private void Awake()
    {
        if (health == null)
        {
            Debug.LogError("Health ScriptableObject not assigned!");
            return;
        }

        currentHealth = health.health;

        if (healthBar == null)
            healthBar = GetComponentInChildren<FloatingHealthBar>();

        UpdateHealthUI();
    }

    public void Hurt(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > health.maxhealth)
            currentHealth = health.maxhealth;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.UpdateHealthBar(currentHealth, health.maxhealth);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);
    }
}
