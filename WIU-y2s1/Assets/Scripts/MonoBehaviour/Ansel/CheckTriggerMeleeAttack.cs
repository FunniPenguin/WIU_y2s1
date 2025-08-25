using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;


public class CheckTriggerMeleeAttack : MonoBehaviour
{
    public _PlayerController _player;
    public string[] targets;

    public void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        foreach (string target in targets)
        {
            if (other.gameObject.CompareTag(target))
            {
                other.gameObject.GetComponent<EntityStatistics>().AddHealth(-_player.GetComponent<EntityStatistics>().damage);
                return;
            }
        }
    }
}