using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
public class PatrolAction : StateAction
{
    public float speed = 2f;
    private bool movingRight = true;

    public override void Act(StateController controller)
    {
        float dir = movingRight ? 1f : -1f;
        controller.transform.Translate(Vector2.right * dir * speed * Time.deltaTime);

        if (Random.value < 0.01f) movingRight = !movingRight;
    }
}
