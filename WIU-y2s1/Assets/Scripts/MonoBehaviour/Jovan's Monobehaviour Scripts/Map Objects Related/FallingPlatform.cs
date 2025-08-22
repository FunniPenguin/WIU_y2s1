using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallWait = 1f;
    public float destroyWait = 2f;

    private bool isFalling;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.bodyType = RigidbodyType2D.Dynamic; // Change the platform to dynamic so it can fall
        yield return new WaitForSeconds(destroyWait);
        yield return null; // Wait for the end of the frame to ensure the platform has fallen: null checker
        gameObject.SetActive(false);// Deactivate the platform after it has fallen and waited
    }
}

// Made by Jovan Yeo Kaisheng
