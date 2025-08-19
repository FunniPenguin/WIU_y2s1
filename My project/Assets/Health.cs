using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Scriptable Objects/Health")]
public class Health : ScriptableObject
{
    public float maxhealth = 100;
    public float health = 100;
}
