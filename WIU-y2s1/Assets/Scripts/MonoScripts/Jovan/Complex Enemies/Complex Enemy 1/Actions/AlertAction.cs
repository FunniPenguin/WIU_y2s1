using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Alert")]
public class AlertAction : StateAction
{
    public float speed = 3.5f;

    public override void Act(StateController controller)
    {
        controller.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
// Made by Jovan Yeo Kaisheng