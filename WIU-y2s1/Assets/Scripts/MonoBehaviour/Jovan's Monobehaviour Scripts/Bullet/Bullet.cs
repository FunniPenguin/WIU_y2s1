using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float forwardDuration = 0.7f; // time flying forward before coming back
    public float lifetime = 3f;          // total bullet lifetime

    private float timer = 0f;
    private bool returning = false;
    private Transform owner; // enemy that shot the bullet

    public void Initialize(Transform shooter)
    {
        owner = shooter;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!returning)
        {
            // Fly straight in local "right" direction
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (timer >= forwardDuration)
            {
                returning = true;
                timer = 0f;
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
            // deal damage both ways (forward & returning)
            Debug.Log("Player hit by bullet!");
            Destroy(gameObject);
        }
    }
}
