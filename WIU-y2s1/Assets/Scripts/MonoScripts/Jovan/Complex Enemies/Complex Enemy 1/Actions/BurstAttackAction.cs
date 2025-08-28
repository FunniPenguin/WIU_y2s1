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

            for (int i = -2; i <= 2; i++)
            {
                float angleOffset = i * 10f;
                Quaternion rot = Quaternion.Euler(0, 0, data.aimAngle + angleOffset);
                GameObject bullet = GameObject.Instantiate(bulletPrefab, data.firePoint.position, rot);

                bullet.GetComponent<Bullet>().Initialize(controller.transform);
            }

            data.StartCooldown();
        }
    }
}
// Made by Jovan Yeo Kaisheng