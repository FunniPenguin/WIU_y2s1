using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Decisions/AttackFinishedDecision2")]
public class AttackFinishedDecision2 : StateDecision
{
    public float attackDuration = 0.5f;

    public override bool Decide(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data == null) return false;

        if (data.attackTimer >= attackDuration)
        {
            data.attackTimer = 0f;
            return true;
        }

        return false;
    }
}
// Made by Jovan Yeo Kaisheng