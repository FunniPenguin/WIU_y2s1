using UnityEngine;

public class BouncyObject : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandleBounce(collision.gameObject);
        }
    }

    private void HandleBounce(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset vertical velocity
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse); // Apply bounce
        }
    }
}
// Made by Jovan Yeo Kaisheng