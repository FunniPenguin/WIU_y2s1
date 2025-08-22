using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModiferSO : CharacterStatModiferSO
{
    // This modifier will heal the character by a specified amount
    public override void AffectCharacter(GameObject character, float val)
    {
            HealthSystem healthSystem = character.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.Heal((int)val); // val is the amount to heal
        }
    }
}
// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.