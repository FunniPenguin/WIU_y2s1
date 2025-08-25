using UnityEngine;

public abstract class StateDecision : ScriptableObject
{
    public abstract bool Decide(StateController controller);
}
