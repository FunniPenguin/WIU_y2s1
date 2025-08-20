using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "InvincibilityEffect", menuName = "Effect/InvincibilityEffect")]
public class InvincibilityEffect : ItemEffect
{
    public float duration = 5f;

    public override void Use(GameObject user)
    {
        HealthSystem healthSystem = user.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.StartCoroutine(ApplyInvincibility(healthSystem));
        }
    }

    private IEnumerator ApplyInvincibility(HealthSystem healthSystem)
    {
        healthSystem.isInvincible = true;
        Debug.Log("Invincible!");
        yield return new WaitForSeconds(duration);
        healthSystem.isInvincible = false;
        Debug.Log("No longer invincible!");
    }
}
