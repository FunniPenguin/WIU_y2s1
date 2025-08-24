using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Chase")]
public class ChaseAction : StateAction
{
    public float chaseSpeed = 4f;

    public override void Act(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
        {
            Vector2 dir = (data.player.position - controller.transform.position).normalized;
            float distance = dir.magnitude;
            if (distance > 0.05f) 
            {
                dir.Normalize();
                controller.transform.Translate(dir * chaseSpeed * Time.deltaTime, Space.World);
            }

            // Flip enemy
            controller.transform.localScale = (data.player.position.x < controller.transform.position.x) ?
                new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }
}