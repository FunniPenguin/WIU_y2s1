using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float forwardDuration = 0.7f; // time flying forward before coming back
    public float lifetime = 3f; // total bullet lifetime
    public float damage = 5f;

    private float timer = 0f;
    private bool returning = false;
    private Transform owner; // enemy that shot the bullet

    [Header("Events")]
    public UnityEvent onSpawn;
    public UnityEvent onHitPlayer;
    public UnityEvent onReturn;

    public void Initialize(Transform shooter)
    {
        owner = shooter;
    }

    void Start()
    {
        onSpawn?.Invoke();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!returning)
        {
            // Fly straight
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (timer >= forwardDuration)
            {
                returning = true;
                timer = 0f;

                onReturn?.Invoke();
            }
        }
        else
        {
            // Return to shooter
            if (owner != null)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    owner.position,
                    speed * Time.deltaTime
                );

                if (Vector2.Distance(transform.position, owner.position) < 0.2f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Hurt(damage); 
            }

            onHitPlayer?.Invoke();
            Destroy(gameObject);
        }
    }
}
// Made by Jovan Yeo Kaisheng