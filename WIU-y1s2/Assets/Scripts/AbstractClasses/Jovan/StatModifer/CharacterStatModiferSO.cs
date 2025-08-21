using UnityEngine;
public abstract class CharacterStatModiferSO : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, float val);
}