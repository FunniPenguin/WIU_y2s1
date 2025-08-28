using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FileBtn : MonoBehaviour
{
    [SerializeField] private int _fileIndex;
    private TextMeshProUGUI _textDisplay;
    private string FileName;
    private GameData _gameData;

    private void Awake()
    {
        _textDisplay = GetComponentInChildren<TextMeshProUGUI>();
        FileName = $"SaveData{_fileIndex}";
    }
    private void Start()
    {
        if (DataPersistenceManager.Instance.CheckIfFileExists(FileName))
        {
            DataPersistenceManager.Instance.SetGameData();
            _gameData = DataPersistenceManager.Instance.GetGameData();
            _textDisplay.text = $"Level: {_gameData._currentLevel + 1};  Playtime: ;";
        }
        else
        {
            _textDisplay.text = "Empty";
        }
    }
    public void StartGameFromLoad()
    {
        if (DataPersistenceManager.Instance.CheckIfFileExists(FileName))
        {
            DataPersistenceManager.Instance.SetGameData();
            _gameData = DataPersistenceManager.Instance.GetGameData();
            GameSceneManager.Instance.LoadLevel(
                GameSceneManager.Instance._levelBuildIndexes[_gameData._currentLevel]);
        }
    }
    public void SaveGame()
    {
        //Calling this function just to set the filemanager filename to this save file name
        DataPersistenceManager.Instance.CheckIfFileExists(FileName);
        //File manager will create a path if does not exist so should be able to save the file.
        DataPersistenceManager.Instance.SaveGame();
        _gameData = DataPersistenceManager.Instance.GetGameData();
        _textDisplay.text = $"Level: {_gameData._currentLevel + 1};  Playtime: ;";
    }
}
