using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Decisions/ReadyToAimDecision")]
public class ReadyToAimDecision : StateDecision
{
    public float attackRange = 3f;
    public float cooldownDuration = 1.0f;

    private float cooldownTimer = 0f;

    public override bool Decide(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data == null || data.player == null) return false;

        cooldownTimer += Time.deltaTime;

        bool inRange = Vector2.Distance(controller.transform.position, data.player.position) <= attackRange;
        bool cooldownDone = cooldownTimer >= cooldownDuration;

        if (inRange && cooldownDone)
        {
            cooldownTimer = 0f;
            return true;
        }

        return false;
    }
}
// Made by Jovan Yeo Kaisheng