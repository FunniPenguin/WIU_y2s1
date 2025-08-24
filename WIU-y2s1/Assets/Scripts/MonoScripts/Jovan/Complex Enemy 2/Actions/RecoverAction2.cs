using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Actions/RecoverAction")]
public class RecoverAction2 : StateAction
{
    public float recoverSpeed = 2f;

    public override void Act(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data == null || data.player == null) return;

        // Increase timer
        data.recoverTimer += Time.deltaTime;

        // Move away from player
        Vector2 dir = (controller.transform.position - data.player.position).normalized;
        controller.transform.position += (Vector3)dir * recoverSpeed * Time.deltaTime;

        // Reset rotation back to normal
        controller.transform.rotation = Quaternion.identity;
    }
}
