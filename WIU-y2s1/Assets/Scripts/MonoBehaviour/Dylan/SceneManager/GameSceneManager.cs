using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    //Start of singleton
    private static GameSceneManager _instance;
    public static GameSceneManager Instance {  get { return _instance; } }
    
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
    //End of singleton

    private int _currMapIndex; //identitify the build index of the level that the player is currently on
    private int _additiveSceneIndex; //track the current additive scene menu opened such as pause menu so that can destroy
    [SerializeField] private int _startingLevelIndex, _pauseMenuIndex; //stores the starting level so that save file knows which starting level to load

    private void Start()
    {
        _currMapIndex = 0;
        _additiveSceneIndex = -1;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (SceneManager.loadedSceneCount != 2)
            { LoadMenu(_pauseMenuIndex); }
            else { UnloadMenu(); }
        }
    }
    public void LoadScene(int sceneIndex)
    {
        //Loads a scene while removing any pause menu,
        //calling scenemanager normally may result in additive scene variables not being reset
        UnloadMenu();
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
    public void LoadScene(string sceneIndex)
    {
        UnloadMenu();
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
    public void SwitchMap(int mapIndex)
    {
        LoadMap(SceneManager.GetActiveScene().buildIndex, mapIndex);
    }
    public void LoadMap(int currentSceneIndex, int nextSceneIndex)
    {
        //To load a map and change the current map index so that the save file knows which map to return to
        //UnloadMenu();
        Debug.Log($"Loading scene: {nextSceneIndex}");

        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
        _currMapIndex = nextSceneIndex;
        Debug.Log($"current map index: {_currMapIndex}");
    }
    public void LoadMenu(int index)
    {
        //check that no other additive scenes have been loaded 
        if (SceneManager.loadedSceneCount != 1)
        {
            return;
        }
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
        //track the additive scene
        _additiveSceneIndex = index;
    }
    public void UnloadMenu()
    {
        //checks whether build index or name was used to add the additive scene and unloads scene based on that
        //resets the value to ensure that additive scene can be loaded
        if (_additiveSceneIndex != -1)
        {
            SceneManager.UnloadSceneAsync(_additiveSceneIndex);
            _additiveSceneIndex = -1;
        }
    }
    public void ReloadCurrentScene()
    {
        //reload scene useful in restarting the level
        //to be used in unity events
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        //for ending the game, to be used in unity events
        Application.Quit();
    }
    public int GetCurrentMapIndex()
    {
        return _currMapIndex; 
    }
    public int GetStartingLevelIndex()
    {
        return _startingLevelIndex;
    }
}

//This class is done by Yap Jun Hong Dylan