using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    private Vector3 nextPosition;
    private Vector3 lastPosition;

    private void Start()
    {
        nextPosition = pointB.position;
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 deltaMove = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime) - transform.position;
        transform.position += deltaMove;

        // Move any player on top
        MovePlayerOnPlatform(deltaMove);

        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }

        lastPosition = transform.position;
    }

    private void MovePlayerOnPlatform(Vector3 deltaMove)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.transform.position += deltaMove;
            }
        }
    }
}
