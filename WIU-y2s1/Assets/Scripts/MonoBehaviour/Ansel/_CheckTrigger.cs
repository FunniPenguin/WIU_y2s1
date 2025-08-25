using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class _CheckTrigger : MonoBehaviour
{
    public _CollisionTarget[] targets;

    public void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        foreach (_CollisionTarget target in targets)
        {
            if (other == target.collider)
            {
                target.onCollisionEnter.Invoke();
                return;
            }
        }
    }

    public void OnTriggerExit2D(UnityEngine.Collider2D other)
    {
        foreach (_CollisionTarget target in targets)
        {
            if (other == target.collider)
            {
                target.onCollisionExit.Invoke();
                return;
            }
        }
    }
}