using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/PlayerInDetectRange")]
public class PlayerInDetectRangeDecision : StateDecision
{
    public float detectRange = 8f;

    public override bool Decide(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
            return Vector2.Distance(controller.transform.position, data.player.position) < detectRange;
        return false;
    }
}
// Made by Jovan Yeo Kaisheng