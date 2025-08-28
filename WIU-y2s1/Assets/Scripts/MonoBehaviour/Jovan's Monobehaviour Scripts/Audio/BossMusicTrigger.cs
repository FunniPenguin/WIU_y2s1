using UnityEngine;
using UnityEngine.Events;

public class BossMusicTrigger : MonoBehaviour
{
    [Header("Unity Events")]
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerExit.Invoke();
        }
    }
}
