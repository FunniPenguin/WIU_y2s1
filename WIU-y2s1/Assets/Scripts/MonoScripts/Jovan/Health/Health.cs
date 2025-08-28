using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Health/Health")]
public class Health : ScriptableObject
{
    public float maxHealth = 100; // Maximum health value for the player or entity
    public float currentHealth = 100; // current health of player
}

// Made by Jovan Yeo Kaisheng
// This code is part of the Health System.