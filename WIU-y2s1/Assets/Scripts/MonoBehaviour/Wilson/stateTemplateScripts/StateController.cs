using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    public float distanceFromPlayer = 30f;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (currentState != null && player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist <= distanceFromPlayer)
            {
                // Only update FSM if within range
                currentState.UpdateState(this);
            }
        }
    }

    public void TransitionToState(State nextstate)
    {
        if (nextstate != remainState)
        {
            currentState = nextstate;
        }
    }
}
