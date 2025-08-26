using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using _Inventory.Model;

[System.Serializable]
public class GameData
{
    int _currentLevel;

    //Do not save player position as player should spawn at player spawn point

    public SerializableDictionary<string, bool> mapGameObjects; //string is the guid, bool is whether the object is active
    public List<InventoryItem> inventoryItems; //string is the item name, int is the quantity
    public SerializableDictionary<string, bool> savePoints; //string is the id, bool is whether to spawn the player
    public GameData()
    {
        _currentLevel = 1;
        //_playerPosition = new Vector3(0, 0, 0);
        //_currMapIndex = GameSceneManager.Instance.GetStartingLevelIndex();
        mapGameObjects = new SerializableDictionary<string, bool>();
        inventoryItems = new List<InventoryItem>();
        savePoints = new SerializableDictionary<string, bool>();
    }
}

//This class is done by Yap Jun Hong Dylan