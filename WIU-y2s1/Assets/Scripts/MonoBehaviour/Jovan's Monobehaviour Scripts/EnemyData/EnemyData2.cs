using UnityEngine;

public class EnemyData2 : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;

    [HideInInspector] public int currentWaypoint = 0;
    [HideInInspector] public float aimTimer = 0f;
    [HideInInspector] public float attackTimer = 0f;
    [HideInInspector] public float recoverTimer = 0f;

    private void OnEnable()
    {
        currentWaypoint = 0;
        aimTimer = 0f;
        attackTimer = 0f;
        recoverTimer = 0f;

        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
                player = foundPlayer.transform;
        }
    }
}
