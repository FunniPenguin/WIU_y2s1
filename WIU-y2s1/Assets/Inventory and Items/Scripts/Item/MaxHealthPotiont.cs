using UnityEngine;

[CreateAssetMenu(fileName = "MaxHealthPotion", menuName = "Effect/MaxHealthPotion")]
public class MaxHealthPotion : ItemEffect
{
    public override void Use(GameObject user)
    {
        HealthSystem healthSystem = user.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.RestoreFullHealth();
        }
    }
}
