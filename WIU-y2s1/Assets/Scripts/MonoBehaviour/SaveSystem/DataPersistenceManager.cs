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
        this._fileManager = new FileManager(Application.persistentDataPath, _fileName);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            SaveGame();
        }
    }
    public void NewGame()
    {
        //Todo: Add a way to have multiple different playthroughs as current implementation only supports one playthrough
        _gameData = new GameData();
    }
    public void LoadGame()
    {        
        //try to load the file, if not then create a new game
        this._gameData = _fileManager.Load();
        if (_gameData == null)
        {
            Debug.Log("Generating new game data");
            NewGame();
        }
        //Load all the objects which contain data to be saved
        //this._DataPersistenceObjects = FindAllDataPersistenceObjects();
        //foreach (IDataPersistence dataPersistenceObj in _DataPersistenceObjects)
        //{
        //    dataPersistenceObj.LoadData(_gameData);
        //}
        LoadMapObjs();
    }
    public void LoadMapObjs()
    {
        //Load all the objects which contain data to be saved
        this._DataPersistenceObjects = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in _DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
    }
    public void SaveGame() {
        Debug.Log("Saving game data");
        foreach (IDataPersistence dataPersistenceObj in _DataPersistenceObjects)
        {
            //Ref will pass the game data into the save data function by reference.
            dataPersistenceObj.SaveData(ref _gameData);
        }
        _gameData._currMapIndex = GameSceneManager.Instance.GetCurrentMapIndex();
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
            DontDestroyOnLoad(gameObject); //Ensures this singleton will be in every scene
        }
    }
    //Ensure that no more instances once scene ends
    private void OnDestroy() { if (this == _instance) { _instance = null; } }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}


//This class is done by Yap Jun Hong Dylan