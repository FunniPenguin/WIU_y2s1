using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedBoostEffect", menuName = "Effect/SpeedBoostEffect")]
public class SpeedBoostEffect : ItemEffect
{
    public float speedMultiplier = 2f;
    public float duration = 6f;

    public override void Use(GameObject user)
    {
        SamplePlayerMovement movement = user.GetComponent<SamplePlayerMovement>();
        if (movement != null) { movement.StartCoroutine(ApplySpeedBoost(movement)); }
    }

    private IEnumerator ApplySpeedBoost(SamplePlayerMovement movement)
    {
        float originalMoveSpeed = movement.moveSpeed;
        movement.moveSpeed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        movement.moveSpeed = originalMoveSpeed;
    }
}
