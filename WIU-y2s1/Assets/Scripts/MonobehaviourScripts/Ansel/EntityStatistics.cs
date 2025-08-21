using UnityEngine;
using UnityEngine.Events;

public class EntityStatistics : MonoBehaviour
{
    //Health is not to be used for player in this case - The player has its own separate HealthSystem

    public float health;
    public float damage;
    public float speed;
    public float jumpPower;

    //This Unity event is meant to be invoked when the entity crosses a threshold of (health <= 0)
    public UnityEvent uponDeath;

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            uponDeath.Invoke();
        }
    }

    //For adding and removing health
    void AddHealth(float addedHealth)
    {
        health += addedHealth;
    }

    //For adding and removing damage
    void AddDamage(float addedDamage)
    {
        damage += addedDamage;
    }

    //For adding and removing speed
    void AddSpeed(float addedSpeed)
    { 
        speed += addedSpeed;
    }

    //For adding and removing jumpPower
    void AddJumpPower(float addedJumpPower)
    {
        jumpPower += addedJumpPower;
    }
}
