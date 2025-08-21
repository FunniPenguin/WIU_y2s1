using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class _CollisionTagTarget
{
    public string tag;

    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionExit;
}

public class _CheckCollideTag : MonoBehaviour
{
    public _CollisionTagTarget[] targets;

    public void OnCollisionEnter2D(Collision2D other)
    {
        foreach (_CollisionTagTarget target in targets)
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
        foreach (_CollisionTagTarget target in targets)
        {
            if (other.gameObject.CompareTag(target.tag))
            {
                target.onCollisionExit.Invoke();
                return;
            }
        }
    }
}