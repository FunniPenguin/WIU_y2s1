using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatInvicibilityModifier : CharacterStatModiferSO
{
    public float duration = 5f;
    public override void AffectCharacter(GameObject character, float val)
    {
        HealthSystem healthSystem = character.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.StartCoroutine(ApplyInvincibility(healthSystem, val));
        }
    }
    private IEnumerator ApplyInvincibility(HealthSystem healthSystem, float val)
    {
        healthSystem.isInvincible = true;
        Debug.Log("Invincible!");
        yield return new WaitForSeconds(duration * val);
        healthSystem.isInvincible = false;
        Debug.Log("No longer invincible!");
    }
}
