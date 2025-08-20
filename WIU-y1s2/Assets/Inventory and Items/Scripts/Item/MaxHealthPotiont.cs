using UnityEngine;

[CreateAssetMenu(fileName = "MaxHealthPotiont", menuName = "Effect/MaxHealthPotiont")]
public class MaxHealthPotion : ItemEffect
{
    public override void Use(GameObject user)
    {
        HealthSystem healthSystem = user.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.health.health = healthSystem.health.maxhealth;
        }
    }
}
