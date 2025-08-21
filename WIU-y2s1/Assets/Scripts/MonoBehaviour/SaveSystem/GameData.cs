using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData
{
    //int _saveIndex;

    //Do not save player position as player should spawn at player spawn point
    //public Vector2 _playerPosition;
    public int _currMapIndex;

    public SerializableDictionary<string, bool> mapGameObjects; //string is the guid, bool is whether the object is active
    public SerializableDictionary<string, int> inventoryItems; //string is the item name, int is the quantity
    public SerializableDictionary<string, bool> inventorySlots;
    public GameData()
    {
        //_saveIndex = -1;
        //_playerPosition = Vector2.zero;
        _currMapIndex = GameSceneManager.Instance.GetStartingLevelIndex();
        mapGameObjects = new SerializableDictionary<string, bool>();
        inventoryItems = new SerializableDictionary<string, int>();
    }
}

//This class is done by Yap Jun Hong Dylan