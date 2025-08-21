using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModiferSO : CharacterStatModiferSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
            HealthSystem healthSystem = character.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Heal((int)val);
            }
    }
}