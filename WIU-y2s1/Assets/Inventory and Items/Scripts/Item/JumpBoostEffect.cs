using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpBoostEffect", menuName = "Effect/JumpBoostEffect")]
public class JumpBoostEffect : ItemEffect
{
    public float jumpMultiplier = 1.5f;
    public float duration = 5f;

    public override void Use(GameObject user)
    {
        SamplePlayerMovement movement = user.GetComponent<SamplePlayerMovement>();
        if (movement != null)
        {
            movement.StartCoroutine(ApplyJumpBoost(movement));
        }
    }

    private IEnumerator ApplyJumpBoost(SamplePlayerMovement movement)
    {
        float originalJumpPower = movement.jumpPower;
        movement.jumpPower *= jumpMultiplier;
        yield return new WaitForSeconds(duration);
        movement.jumpPower = originalJumpPower;
    }
}
