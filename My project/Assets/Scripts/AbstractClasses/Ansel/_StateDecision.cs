using UnityEngine;

public abstract class _StateDecision : ScriptableObject
{
    public abstract bool Decide(_StateController controller);
}
