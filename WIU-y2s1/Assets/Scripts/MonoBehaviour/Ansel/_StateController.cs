using UnityEngine;

public class _StateController : MonoBehaviour
{
    public _State currentState;
    public _State remainState;

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(_State nextstate)
    {
        if (nextstate != remainState)
        {
            currentState = nextstate;
        }
    }
}
