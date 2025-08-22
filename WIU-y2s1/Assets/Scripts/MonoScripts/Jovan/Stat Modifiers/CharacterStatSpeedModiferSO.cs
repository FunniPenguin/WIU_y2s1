using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatSpeedModiferSO : CharacterStatModiferSO
{
    // This modifier will boost the character's movement speed for a specified duration
    public float speedModifier = 1.5f;
    public float duration = 5f;
    public override void AffectCharacter(GameObject character, float val)
    {
        SamplePlayerMovement movement = character.GetComponent<SamplePlayerMovement>();
        if (movement != null)
        {
            movement.StartCoroutine(ApplySpeedBoost(movement, val)); // Start the coroutine to apply the speed boost
        }
    }

    private IEnumerator ApplySpeedBoost(SamplePlayerMovement movement, float val)
    {
        // Store the original move speed and apply the modified move speed
        float originalMoveSpeed = movement.moveSpeed;
        movement.moveSpeed *= speedModifier * val;
        yield return new WaitForSeconds(duration);
        // After the duration, reset the move speed to the original value
        movement.moveSpeed = originalMoveSpeed;
    }
}
