using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageCooldown = 1f;

    private float lastDamageTime = -999f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    playerHealth.Hurt(damageAmount);
                    Debug.Log("Player Damaged");
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
