using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Platform to move back and forth between two points (pointA and pointB).
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    private Vector3 nextPosition;

    private void Start()
    {
        nextPosition = pointB.position; // Start moving towards pointB
    }

    private void Update()
    {
        // If the platform is not moving, return
        Vector3 deltaMove = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime) - transform.position;
        transform.position += deltaMove;

        // Move any player on top
        MovePlayerOnPlatform(deltaMove);

        // Check if the platform has reached the next position then switch to the other point
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void MovePlayerOnPlatform(Vector3 deltaMove)
    {
        // Move any player on top of the platform
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 1f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.transform.position += deltaMove;
            }
        }
    }
}
// Made by Jovan Yeo Kaisheng
