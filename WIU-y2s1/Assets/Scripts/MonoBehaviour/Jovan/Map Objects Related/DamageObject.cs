using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageCooldown = 1f;

    private float lastDamageTime = -999f; // Initialize to a very low value to allow immediate damage on first contact

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the object colliding with this is the player
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    playerHealth.Hurt(damageAmount); // Apply damage to the player
                    Debug.Log("Player Damaged");
                    lastDamageTime = Time.time; // Update the last damage time to prevent spamming damage
                }
            }
        }
    }
}
// Made by Jovan Yeo Kaisheng
