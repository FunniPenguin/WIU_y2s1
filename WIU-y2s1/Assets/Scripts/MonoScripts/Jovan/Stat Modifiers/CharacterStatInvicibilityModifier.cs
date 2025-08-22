using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatInvicibilityModifierSO : CharacterStatModiferSO
{
    // This modifier will make the character invincible for a specified duration
    public float duration = 5f;
    public override void AffectCharacter(GameObject character, float val)
    {
        // Ensure the character has a HealthSystem component
        HealthSystem healthSystem = character.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.StartCoroutine(ApplyInvincibility(healthSystem, val)); // Start the coroutine to apply invincibility
        }
    }
    private IEnumerator ApplyInvincibility(HealthSystem healthSystem, float val)
    {
        // Set the health system to invincible for the specified duration
        healthSystem.isInvincible = true;
        Debug.Log("Invincible!");
        // Wait for the duration multiplied by the value (val)
        yield return new WaitForSeconds(duration * val);
        // After the duration, set invincibility to false
        healthSystem.isInvincible = false;
        Debug.Log("No longer invincible!");
    }
}
// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.