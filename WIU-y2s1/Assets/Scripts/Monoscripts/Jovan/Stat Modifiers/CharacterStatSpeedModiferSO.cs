using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatSpeedModiferSO : CharacterStatModiferSO
{
    public float speedModifier = 1.5f;
    public float duration = 5f;
    public override void AffectCharacter(GameObject character, float val)
    {
        SamplePlayerMovement movement = character.GetComponent<SamplePlayerMovement>();
        if (movement != null)
        {
            movement.StartCoroutine(ApplySpeedBoost(movement, val));
        }
    }

    private IEnumerator ApplySpeedBoost(SamplePlayerMovement movement, float val)
    {
        float originalMoveSpeed = movement.moveSpeed;
        movement.moveSpeed *= speedModifier * val;
        yield return new WaitForSeconds(duration);
        movement.moveSpeed = originalMoveSpeed;
    }
}
