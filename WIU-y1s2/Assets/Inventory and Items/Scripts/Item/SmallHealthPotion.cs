using UnityEngine;

[CreateAssetMenu(fileName = "SmallHealthPotion", menuName = "Effect/SmallHealthPotion")]
public class SmallHealthPotion : ItemEffect
{
    public float healAmount = 10f;
    public override void Use(GameObject user)
    {
        HealthSystem healthSystem = user.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.Heal(healAmount);
        }
    }
}
