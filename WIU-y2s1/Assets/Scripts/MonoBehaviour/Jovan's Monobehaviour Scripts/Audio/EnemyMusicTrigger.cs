using UnityEngine;
using UnityEngine.Events;

public class EnemyMusicTrigger : MonoBehaviour
{
    [Header("Unity Events")]
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;

    private bool isInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInside)
        {
            isInside = true;
            onPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isInside)
        {
            isInside = false;
            onPlayerExit.Invoke();
        }
    }
}
// Made by Jovan Yeo Kaisheng