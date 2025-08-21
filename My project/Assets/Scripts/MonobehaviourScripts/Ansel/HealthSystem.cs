using UnityEngine;

public class _HealthSystem : MonoBehaviour
{
    public _Health health;

    //[SerializeField] FloatingHealthBar healthBar;
    private void Start()
    {
        health.health = health.maxhealth;
        //healthBar.UpdateHealthBar(health.health, health.maxhealth);
        //healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    public void Hurt(float hurtpoints)
    {
        health.health -= hurtpoints;
        //healthBar.UpdateHealthBar(health.health, health.maxhealth);
    }

    public void Heal(float healpoints)
    {
        health.health += healpoints;
    }

    private void Update()
    {
        if (health.health <= 0)
        {

        }

        if (health.health > health.maxhealth) 
        { 
            health.health = health.maxhealth;
        }
        //healthBar.UpdateHealthBar(health.health, health.maxhealth);
    }
}
