using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> _keys = new List<TKey>();
    [SerializeField] private List<TValue> _values = new List<TValue>();
    
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            _keys.Add(pair.Key);
            _values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        this.Clear();
        if (Keys.Count != Values.Count) {
            Debug.LogError($"Key count and value count do not match. Key count is: {Keys.Count} while value count is: {Values.Count}");
        }
        for (int i = 0; i < Keys.Count; i++)
        {
            this.Add(_keys[i], _values[i]);
        }
    }
}

//This class is done by Yap Jun Hong Dylan