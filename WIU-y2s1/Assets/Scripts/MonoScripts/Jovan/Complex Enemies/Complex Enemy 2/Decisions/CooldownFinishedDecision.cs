using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Decisions/CooldownFinishedDecision")]
public class CooldownFinishedDecision : StateDecision
{
    public float cooldownDuration = 1f;

    public override bool Decide(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data == null) return false;

        data.attackTimer += Time.deltaTime;
        return data.attackTimer >= cooldownDuration;
    }
}
// Made by Jovan Yeo Kaisheng