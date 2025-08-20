using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemData;

    public ItemInstance(ItemData data) 
    { 
        itemData = data; 
    }

    public void Use(GameObject user) 
    {
        if (itemData.effect != null)
        { 
            itemData.effect.Use(user);
        }

        if (itemData.animationFrames != null && itemData.animationFrames.Length > 0) 
        {
            GameObject obj = new GameObject(itemData.itemName + "_Animation");
            obj.transform.position = user.transform.position + Vector3.up;

            SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
            obj.AddComponent<AnimatedSprite>().SetUp(itemData.animationFrames, itemData.frameRate, 0.2f, 0.5f, 3f);
        }
    }

    
}
