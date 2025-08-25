using UnityEngine;
using UnityEngine.Events;

public class EntityStatistics : MonoBehaviour
{
    //Health is not to be used for player in this case - The player has its own separate HealthSystem

    public float health;
    public float damage;
    public float speed;
    public float jumpPower;

    public bool isInvincible = false; // flag to check if enemy is invincible
    [SerializeField] private float _iFrameDuration = 0.5f;
    private float _iFrameTime = 0;
    //This Unity event is meant to be invoked when the entity crosses a threshold of (health <= 0)
    public UnityEvent uponDeath;

    private void Update()
    {
        _iFrameTime+= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (health <= 0)
        {
            uponDeath.Invoke();
        }
    }

    //For adding and removing health
    public void AddHealth(float addedHealth)
    {
        if (addedHealth < 0)
        {
            if (_iFrameTime > _iFrameDuration)
            {
                health += addedHealth;
                _iFrameTime = 0;
            }
        }
        else { health += addedHealth; }
    }

    //For adding and removing damage
    public void AddDamage(float addedDamage)
    {
        damage += addedDamage;
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
}
