using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Actions/ChaseAction")]
public class ChaseAction2 : StateAction
{
    public float chaseSpeed = 3.5f;

    public override void Act(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data.player == null) return;

        controller.transform.position = Vector2.MoveTowards(controller.transform.position,data.player.position,chaseSpeed * Time.deltaTime);
    }
}
