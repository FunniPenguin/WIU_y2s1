using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/PlayerInAttackRange")]
public class PlayerInAttackRangeDecision : StateDecision
{
    public float attackRange = 5f;

    public override bool Decide(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
            return Vector2.Distance(controller.transform.position, data.player.position) < attackRange;
        return false;
    }
}
// Made by Jovan Yeo Kaisheng