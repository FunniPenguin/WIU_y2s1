using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatJumpModiferSO : CharacterStatModiferSO
{
    // This modifier will boost the character's jump power for a specified duration
    public float jumpModifer = 1.5f;
    public float duration = 5f;

    public override void AffectCharacter(GameObject character, float val)
    {
        _PlayerController movement = character.GetComponent<_PlayerController>();
        if (movement != null)
        {
            movement.StartCoroutine(ApplyJumpBoost(movement, val));  // Start the coroutine to apply the jump boost
        }
    }

    private IEnumerator ApplyJumpBoost(_PlayerController movement, float val)
    {
        // Store the original jump power and apply the modified jump power
        float originalJumpPower = movement.jumpHeight;
        movement.jumpHeight *= jumpModifer * val;
        yield return new WaitForSeconds(duration);
        // After the duration, reset the jump power to the original value
        movement.jumpHeight = originalJumpPower;
    }
}
// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.