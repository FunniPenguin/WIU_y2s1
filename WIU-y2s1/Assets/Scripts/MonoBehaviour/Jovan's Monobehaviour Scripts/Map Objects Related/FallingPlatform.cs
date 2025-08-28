using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallWait = 1f;
    public float destroyWait = 2f;
    public float respawnWait = 3f;

    private bool isFalling;
    private Rigidbody2D rb;
    private Vector3 startPosition;
    private RigidbodyType2D originalBodyType;

    private Renderer rend;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();

        startPosition = transform.position;
        originalBodyType = rb.bodyType;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallWait);

        // change to dynamic to fall
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(destroyWait);

        // Hide platform
        rend.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(respawnWait);
        ResetPlatform();
    }

    private void ResetPlatform()
    {
        transform.position = startPosition;

        // Clear velocities due to falling
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // return Static
        rb.bodyType = originalBodyType;

        // Show platform
        rend.enabled = true;
        col.enabled = true;

        isFalling = false;
    }
}
// Made by Jovan Yeo Kaisheng