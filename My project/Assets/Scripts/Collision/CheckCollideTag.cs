using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class CollisionTagTarget
{
    public string tag;

    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionExit;
}

public class CheckCollideTag : MonoBehaviour
{
    public CollisionTagTarget[] targets;

    public void OnCollisionEnter2D(Collision2D other)
    {
        foreach (CollisionTagTarget target in targets)
        {
            if (other.gameObject.CompareTag(target.tag))
            {
                target.onCollisionEnter.Invoke();
                return;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        foreach (CollisionTagTarget target in targets)
        {
            if (other.gameObject.CompareTag(target.tag))
            {
                target.onCollisionExit.Invoke();
                return;
            }
        }
    }
}