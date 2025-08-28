using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnDestroy() { if (this == _instance) { _instance = null; } }


    //Reference to the consolidated game data so that it is easier to save to json when all the data is in one place
    private GameData _gameData;

    //List of game objects that require saving
    private List<IDataPersistence> _DataPersistenceObjects;
    public bool CheckIfFileExists(string fileName)
    {
        this._fileManager = new FileManager(Application.persistentDataPath, fileName);
        //try to load the file        
        if (_fileManager.Load() == null)
        {
            //Return false to tell the UI not to display any statistics
            return false;
            //NewGame();
        }
        return true;
    }
    public GameData GetGameData()
    {
        return _gameData;
    }
    public void SetGameData()
    {
        _gameData = _fileManager.Load();
        if (_gameData == null)
        {
            Debug.LogError("No game data loaded");
            return;
        }

    }
    public void NewGame()
    {
        _gameData = new GameData();
    }
    public void Load()
    {
        if (_gameData == null)
        {
            Debug.LogError("No game data loaded");
            return;
        }
        //Load all the objects which contain data to be saved
        this._DataPersistenceObjects = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in _DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
    }
    public void SaveGame() {
        //Debug.Log("Saving game data");
        foreach (IDataPersistence dataPersistenceObj in _DataPersistenceObjects)
        {
            //Ref will pass the game data into the save data function by reference.
            dataPersistenceObj.SaveData(_gameData);
        }
        _gameData._lastSave = System.DateTime.Now;
        _fileManager.Save(_gameData);
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public static string GenerateGUID()
    {
        int RandomInteger = Random.Range(1, 3);
        char alphabet;
        switch (RandomInteger)
        {
            case 1:
                alphabet = 'A';
                break;
            case 2:
                alphabet = 'B';
                break;
            case 3:
                alphabet = 'C';
                break;
            default:
                alphabet = 'A';
                break;
        }
        int SevenDigit = Random.Range(1000000, 9999999);
        return new string(alphabet + SevenDigit.ToString());
    }
}


//This class is done by Yap Jun Hong Dylan