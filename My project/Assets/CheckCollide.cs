using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class CollisionTarget
{
    public Collider2D collider;

    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionExit;
}

public class CheckCollide : MonoBehaviour
{
    public CollisionTarget[] targets;

    public void OnCollisionEnter2D(Collision2D other)
    {
        foreach (CollisionTarget target in targets)
        {
            if (other.collider == target.collider)
            {
                target.onCollisionEnter.Invoke();
                return;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        foreach (CollisionTarget target in targets)
        {
            if (other.collider == target.collider)
            {
                target.onCollisionExit.Invoke();
                return;
            }
        }
    }
}