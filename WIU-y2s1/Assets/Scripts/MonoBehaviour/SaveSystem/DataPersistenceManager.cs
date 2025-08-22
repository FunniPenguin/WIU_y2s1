using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

//Making the class a singleton class
//This implementation requires instantiating a DataPersistenceManager game object inside each scene
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;
    private FileManager _fileManager;

    //Static reference to the class instance, the instance created by the game object.
    //If more than one game object has this instance, will be removed, to access this instance use the public static reference
    private static DataPersistenceManager _instance;

    //Public static reference to _instance created.
    public static DataPersistenceManager Instance { get { return _instance; } }

    //Reference to the consolidated game data so that it is easier to save to json when all the data is in one place
    private GameData _gameData;

    //List of game objects that require saving
    private List<IDataPersistence> _DataPersistenceObjects;

    /**************************************************************************************************************
     * Note: This script is just a basic implementation to load save file when game starts and save when game ends,
     * Mod this such that game is saved only when player enters save menu to save and new game and load are handled in start
     **************************************************************************************************************/
    private void Start()
    {
        this._DataPersistenceObjects = FindAllDataPersistenceObjects();
        this._fileManager = new FileManager(Application.persistentDataPath, _fileName);
        LoadGame();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        //Todo: Add a way to have multiple different playthroughs as current implementation only supports one playthrough
        _gameData = new GameData();
    }
    public void LoadGame()
    {
        this._gameData = _fileManager.Load();
        if (_gameData == null)
        {
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in _DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
    }
    public void SaveGame() {
        foreach (IDataPersistence dataPersistenceObj in _DataPersistenceObjects)
        {
            //Ref will pass the game data into the save data function by reference.
            dataPersistenceObj.SaveData(ref _gameData);
        }
        _fileManager.Save(_gameData);
    }



    private void Awake()
    {
        //Checking if _instance was already initialised in the scene, if so then destroy this DataPersistenceManager
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    //Ensure that no more instances once scene ends
    private void OnDestroy() { if (this == _instance) { _instance = null; } }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
