using UnityEngine;

[System.Serializable]
public class StateTransition
{
    public StateDecision decision;
    public State truestate;
    public State falsestate;
}
