using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "_Health", menuName = "Scriptable Objects/_Health")]
public class _Health : ScriptableObject
{
    public float maxhealth = 100;
    public float health = 100;
}
