using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileMovement : MonoBehaviour
{
    public Vector2 _moveDirection;
    public float _fireballSpeed = 5;
    public float _fireballDamage = 5;

    private float lifetime = 3;
    private float lifetimer = 0;

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
