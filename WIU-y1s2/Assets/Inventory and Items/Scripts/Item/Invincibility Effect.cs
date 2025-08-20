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
            user.GetComponent<MonoBehaviour>().StartCoroutine(ApplyInvincibility(healthSystem));
        }
    }

    private IEnumerator ApplyInvincibility (HealthSystem healthSystem)
    {
        healthSystem.enabled = false;
        yield return new WaitForSeconds(duration);
        healthSystem.enabled = true;
    }
}
