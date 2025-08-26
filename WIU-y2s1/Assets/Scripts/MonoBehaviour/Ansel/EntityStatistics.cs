using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class EntityStatistics : MonoBehaviour
{
    //Health is not to be used for player in this case - The player has its own separate HealthSystem

    [Header("Stats")]
    public float maxHealth = 100f;
    [SerializeField] private float health;
    public float damage;
    public float speed;
    public float jumpPower;

    [Header("Invincibility")]
    public bool isInvincible = false; // flag to check if enemy is invincible
    [SerializeField] private float iFrameDuration = 0.5f;
    private float iFrameTimer = 0f;
    //This Unity event is meant to be invoked when the entity crosses a threshold of (health <= 0)

    [Header("Events")]
    public UnityEvent uponDeath;
    [SerializeField] private StatusBar statusBar;

    private float originalDamage;

    private void Awake()
    {
        health = maxHealth; // start with full health
        originalDamage = damage; // to make sure that the original damage is stored so that damage can be reset when equipping a different weapon
        if (statusBar == null)
            statusBar = GetComponentInChildren<StatusBar>();
        UpdateUI();
    }
    private void Update()
    {
        iFrameTimer += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    //For adding and removing health
    public void AddHealth(float value)
    {
        if (value < 0) // Taking damage
        {
            if (iFrameTimer > iFrameDuration && !isInvincible)
            {
                health = Mathf.Clamp(health + value, 0, maxHealth);
                iFrameTimer = 0f;
                UpdateUI();
            }
        }
        else // Healing
        {
            health = Mathf.Clamp(health + value, 0, maxHealth);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (statusBar != null)
        {
            statusBar.UpdateStatusBar(health, maxHealth);
        }
    }

    //For adding and removing damage
    public void AddDamage(float addedDamage)
    {
        damage += addedDamage;
    }

    //For resetting damage when unequipping weapon
    public void ResetDamage()
    {
        damage = originalDamage;
    }

    //For adding and removing speed
    public void AddSpeed(float addedSpeed)
    { 
        speed += addedSpeed;
    }

    //For adding and removing jumpPower
    public void AddJumpPower(float addedJumpPower)
    {
        jumpPower += addedJumpPower;
    }

    private void Die()
    {
        uponDeath.Invoke();
        gameObject.SetActive(false);
    }
}
