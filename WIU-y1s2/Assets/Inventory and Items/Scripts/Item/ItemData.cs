using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite[] animationFrames;
    public float frameRate = 10f;

    public ItemEffect effect;
}
