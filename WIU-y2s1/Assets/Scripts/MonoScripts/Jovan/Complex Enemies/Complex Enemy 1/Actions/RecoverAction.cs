using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Recover")]
public class RecoverAction : StateAction
{
    public float backSpeed = 2f;

    public override void Act(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.player != null)
        {
            Vector2 dir = (controller.transform.position - data.player.position).normalized;
            controller.transform.Translate(dir * backSpeed * Time.deltaTime);
        }
    }
}
// Made by Jovan Yeo Kaisheng