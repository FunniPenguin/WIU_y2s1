using UnityEngine;

/// <summary>
/// Base class for all character stat modifiers.
/// You can implement this ScriptableObject to apply a specific effect
/// (e.g., increase health, boost speed, raise attack power).
/// </summary>

public abstract class CharacterStatModiferSO : ScriptableObject
{

    // It will apply a modifier to the character's stats.
    // Which will be applied to their derived classes. u can call them with the method AffectCharacter.
    // For Naming conventions for this class, you can use the following: public class CharacterStat(buffname)ModiferSO : CharacterStatModiferSO
    public abstract void AffectCharacter(GameObject character, float val);
}

// Made by Jovan Yeo Kaisheng
// This code is part of the _Inventory system.