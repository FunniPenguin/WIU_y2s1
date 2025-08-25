using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Decisions/PlayerInAttackRange")]
public class PlayerInAttackRange2Decision : StateDecision
{
    public float attackRange = 3f;

    public override bool Decide(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data.player == null) return false;

        return Vector2.Distance(controller.transform.position, data.player.position) <= attackRange;
    }
}
