using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] public List<TKey> keys = new List<TKey>();
    [SerializeField] public List<TValue> values = new List<TValue>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
            Debug.Log($"Added {pair.Key} and {pair.Value} into the dictionary");
        }
    }
    public void OnAfterDeserialize()
    {
        //this.Clear();
        if (Keys.Count != Values.Count)
        {
            Debug.LogError($"Key count and value count do not match. Key count is: {Keys.Count} while value count is: {Values.Count}");
        }
        Debug.Log($"{Keys.Count} keys, {Values.Count} values");
        for (int i = 0; i < Keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
            Debug.Log($"Loaded {keys[i]} and {values[i]} into the dictionary");
        }
    }
}

//This class is done by Yap Jun Hong Dylan