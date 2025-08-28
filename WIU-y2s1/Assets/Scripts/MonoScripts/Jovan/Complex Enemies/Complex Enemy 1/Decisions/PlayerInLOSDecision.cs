using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/PlayerInLOS")]
public class PlayerInLOSDecision : StateDecision
{
    public LayerMask obstacleMask;
    public override bool Decide(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
        {
            Vector2 dir = data.player.position - controller.transform.position;
            float dist = dir.magnitude;
            return !Physics2D.Raycast(controller.transform.position, dir, dist, obstacleMask);
        }
        return false;
    }
}
// Made by Jovan Yeo Kaisheng