using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/PlayerInMeleeRange")]
public class PlayerInMeleeRangeDecision : StateDecision
{
    public float meleeRange = 2f;

    public override bool Decide(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
            return Vector2.Distance(controller.transform.position, data.player.position) < meleeRange;
        return false;
    }
}
// Made by Jovan Yeo Kaisheng