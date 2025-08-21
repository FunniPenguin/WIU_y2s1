using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatJumpModiferSO : CharacterStatModiferSO
{
    public float jumpModifer = 1.5f;
    public float duration = 5f;

    public override void AffectCharacter(GameObject character, float val)
    {
        SamplePlayerMovement movement = character.GetComponent<SamplePlayerMovement>();
        if (movement != null)
        {
            movement.StartCoroutine(ApplyJumpBoost(movement, val));
        }
    }

    private IEnumerator ApplyJumpBoost(SamplePlayerMovement movement, float val)
    {
        float originalJumpPower = movement.jumpPower;
        movement.jumpPower *= jumpModifer * val;
        yield return new WaitForSeconds(duration);
        movement.jumpPower = originalJumpPower;
    }
}
