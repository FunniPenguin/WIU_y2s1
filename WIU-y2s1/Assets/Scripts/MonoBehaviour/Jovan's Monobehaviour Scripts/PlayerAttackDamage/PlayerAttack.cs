using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EntityStatistics enemyStats = other.GetComponent<EntityStatistics>();
        if (enemyStats != null)
        {
            enemyStats.AddHealth(-damage); // subtract health
        }
    }
}
