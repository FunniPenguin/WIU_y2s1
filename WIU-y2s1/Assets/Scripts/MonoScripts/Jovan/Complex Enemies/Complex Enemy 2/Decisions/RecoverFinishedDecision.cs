using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Decisions/RecoverFinished")]
public class RecoverFinishedDecision : StateDecision
{
    public float recoverDuration = 1.5f;

    public override bool Decide(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data == null) return false;

        bool finished = data.recoverTimer >= recoverDuration;

        if (finished)
            data.recoverTimer = 0f;

        return finished;
    }
}
