using UnityEngine;
using UnityEngine.Events;

public class ProjectileMovement : MonoBehaviour
{
    public Vector2 _moveDirection;
    public float _fireballSpeed = 5;
    public float _fireballDamage = 5;

    private float lifetime = 3;
    private float lifetimer = 0;

    // Added by Jovan Yeo Kaisheng
    [Header("Events")]
    public UnityEvent onSpawn;
    public UnityEvent onHitEnemy;

    void Start()
    {
        onSpawn?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        lifetimer += Time.deltaTime;

        this.transform.Translate(_moveDirection * _fireballSpeed * Time.deltaTime);

        if (lifetimer >= lifetime )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EntityStatistics healthSystem = collision.GetComponent<EntityStatistics>();
            if (healthSystem != null)
            {
                healthSystem.AddHealth(-_fireballDamage);
            }

            onHitEnemy?.Invoke();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Boss"))
        {
            EntityStatistics healthSystem = collision.GetComponent<EntityStatistics>();
            if (healthSystem != null)
            {
                healthSystem.AddHealth(-_fireballDamage);
            }

            onHitEnemy?.Invoke();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            return;
        }
        else if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
