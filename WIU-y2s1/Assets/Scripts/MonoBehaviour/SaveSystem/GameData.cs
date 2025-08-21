using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData
{
    //int _saveIndex;

    Vector2 _playerPosition;
    public int _currMapIndex;

    SerializableDictionary<string, bool> mapGameObjects; //string is the guid, bool is whether the object is active
    SerializableDictionary<string, int> inventoryItems; //string is the item name, int is the quantity

    public GameData()
    {
        //_saveIndex = -1;
        _playerPosition = Vector2.zero;
        _currMapIndex = GameSceneManager.Instance.GetStartingLevelIndex();
        mapGameObjects = new SerializableDictionary<string, bool>();
        inventoryItems = new SerializableDictionary<string, int>();
    }
}

//This class is done by Yap Jun Hong Dylan