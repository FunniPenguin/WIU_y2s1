using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite[] animationFrames;
    public float frameRate = 10f;

    public ItemEffect effect;

    public Sprite Icon
    {
        get
        {
            if (animationFrames != null && animationFrames.Length > 0)
                return animationFrames[0];
            return null;
        }
    }
}
