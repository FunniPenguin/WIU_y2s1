using UnityEngine;
using UnityEngine.Events;

public class EnemyData2 : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;
    public float damage = 5;

    [HideInInspector] public int currentWaypoint = 0;
    [HideInInspector] public float aimTimer = 0f;
    [HideInInspector] public float attackTimer = 0f;
    [HideInInspector] public float recoverTimer = 0f;

    public UnityEvent onAttack;

    private void OnEnable()
    {
        currentWaypoint = 0;
        aimTimer = 0f;
        attackTimer = 0f;
        recoverTimer = 0f;

        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Hurt(damage);
                onAttack?.Invoke();
            }
        }
    }
}
// Made by Jovan Yeo Kaisheng