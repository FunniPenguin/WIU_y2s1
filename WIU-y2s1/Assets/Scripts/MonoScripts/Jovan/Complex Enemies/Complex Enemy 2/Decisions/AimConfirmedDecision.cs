using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Decisions/AimConfirmed")]
public class AimConfirmedDecision : StateDecision
{
    public float aimDuration = 1.5f;

    public override bool Decide(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        return data != null && data.aimTimer >= aimDuration;
    }
}
