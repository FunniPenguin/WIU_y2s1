using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Retreat")]
public class RetreatAction : StateAction
{
    public float retreatSpeed = 3f;

    public override void Act(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
        {
            Vector2 dir = (controller.transform.position - data.player.position);
            float distance = dir.magnitude;
            if (distance > 0.05f)
            {
                dir.Normalize();
                controller.transform.Translate(dir * retreatSpeed * Time.deltaTime, Space.World);
            }

            controller.transform.localScale = (data.player.position.x < controller.transform.position.x) ?
                new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }
    }
}
// Made by Jovan Yeo Kaisheng