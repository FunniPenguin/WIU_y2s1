using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Aim")]
public class AimAction : StateAction
{
    public override void Act(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
        {
            Vector2 dir = (data.player.position - controller.transform.position).normalized;
            data.aimAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (data.player.position.x < controller.transform.position.x)
                controller.transform.localScale = new Vector3(1, 1, 1);
            else
                controller.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
