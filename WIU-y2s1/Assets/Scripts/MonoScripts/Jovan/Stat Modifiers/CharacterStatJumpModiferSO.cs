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
        SamplePlayerMovement movement = character.GetComponent<SamplePlayerMovement>();
        if (movement != null)
        {
            movement.StartCoroutine(ApplyJumpBoost(movement, val));  // Start the coroutine to apply the jump boost
        }
    }

    private IEnumerator ApplyJumpBoost(SamplePlayerMovement movement, float val)
    {
        // Store the original jump power and apply the modified jump power
        float originalJumpPower = movement.jumpPower;
        movement.jumpPower *= jumpModifer * val;
        yield return new WaitForSeconds(duration);
        // After the duration, reset the jump power to the original value
        movement.jumpPower = originalJumpPower;
    }
}
