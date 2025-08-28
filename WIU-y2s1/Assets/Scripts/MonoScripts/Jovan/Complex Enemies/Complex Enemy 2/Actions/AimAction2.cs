using UnityEngine;

[CreateAssetMenu(menuName = "FSM2/Actions/AimAction")]
public class AimAction2 : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyData2 data = controller.GetComponent<EnemyData2>();

        if (data == null || data.player == null) return;

        if (data.aimTimer <= 0f)
            data.aimTimer = 0f;

        data.aimTimer += Time.deltaTime;
    }
}
// Made by Jovan Yeo Kaisheng