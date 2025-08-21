using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextstate)
    {
        if (nextstate != remainState)
        {
            currentState = nextstate;
        }
    }
}
