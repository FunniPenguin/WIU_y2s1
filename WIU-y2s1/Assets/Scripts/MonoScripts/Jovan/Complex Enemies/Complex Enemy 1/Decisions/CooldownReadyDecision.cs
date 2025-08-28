using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/CooldownReady")]
public class CooldownReadyDecision : StateDecision
{
    public override bool Decide(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data))
            return data.IsCooldownReady();
        return false;
    }
}
// Made by Jovan Yeo Kaisheng