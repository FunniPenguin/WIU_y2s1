using UnityEngine;

public abstract class _StateAction : ScriptableObject
{
    public abstract void Act(_StateController controller);
}
