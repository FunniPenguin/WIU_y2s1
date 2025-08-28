using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Actions/AttackAction2")]
public class AttackAction2 : StateAction
{
    public float attackSpeed = 6f;

    public override void Act(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();
        if (data == null || data.player == null) return;

        // Reset timer automatically when entering
        if (data.attackTimer <= 0f)
            data.attackTimer = 0f;

        // Increment timer
        data.attackTimer += Time.deltaTime;

        // Move toward player
        controller.transform.position = Vector2.MoveTowards(controller.transform.position,data.player.position, attackSpeed * Time.deltaTime);

        // Rotate to face the player
        Vector2 direction = (data.player.position - controller.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        controller.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
// Made by Jovan Yeo Kaisheng