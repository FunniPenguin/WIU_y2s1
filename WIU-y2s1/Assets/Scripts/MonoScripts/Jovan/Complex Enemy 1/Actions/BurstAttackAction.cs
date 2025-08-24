using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/BurstAttack")]
public class BurstAttackAction : StateAction
{
    public GameObject bulletPrefab;

    public override void Act(StateController controller)
    {
        if (controller.TryGetComponent(out EnemyData data) && data.CanAttack())
        {
            if (data.firePoint == null) return;

            for (int i = -1; i <= 1; i++)
            {
                float angleOffset = i * 35f;
                Quaternion rot = Quaternion.Euler(0, 0, data.aimAngle + angleOffset);
                GameObject.Instantiate(bulletPrefab, data.firePoint.position, rot);
            }

            data.StartCooldown();
        }
    }
}
