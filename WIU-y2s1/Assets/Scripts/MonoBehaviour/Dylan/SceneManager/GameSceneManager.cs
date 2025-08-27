using System.Collections;
using Unity.Hierarchy;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    private int _additiveSceneIndex; //track the current additive scene menu opened such as pause menu so that can destroy
    [SerializeField] private int _startingLevelIndex, _pauseMenuIndex, _loadSceneIndex; //stores the starting level so that save file knows which starting level to load
    private bool _enablePauseMenu = false;
    private void Start()
    {
        _additiveSceneIndex = -1;
        _enablePauseMenu = false;
    }
    private void Update()
    {
        if (_enablePauseMenu) {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (SceneManager.loadedSceneCount != 2)
                { LoadMenu(_pauseMenuIndex); }
                else { UnloadMenu(); }
            }
        }
    }
    public void LoadScene(int sceneIndex)
    {
        //Loads a scene while removing any pause menu,
        //calling scenemanager normally may result in additive scene variables not being reset
        UnloadMenu();
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
    public void LoadLevel(int LevelIndex)
    {
        UnloadMenu();
        SceneManager.LoadScene(_loadSceneIndex, LoadSceneMode.Additive);
        StartCoroutine(LoadAsyncScene(LevelIndex));
        SceneManager.UnloadSceneAsync(_loadSceneIndex);
    }
    public void LoadMenu(int index)
    {
        //check that no other additive scenes have been loaded 
        if (SceneManager.loadedSceneCount != 1)
        {
            return;
        }
        //Disable active scene event system so that pause menu event system will get all the input
        ToggleSceneEventSystem(false);
        //Load above the active scene
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
        //Ensure the active scene is not updated
        Time.timeScale = 0;
        //track the additive scene
        _additiveSceneIndex = index;
    }
    //Function to switch from pause menu to other menus like save menu and inventory menu and also to switch back from those menus
    public void SwitchMenu(int index)
    {
        Debug.Log("Attempting to switch menus");
        if (SceneManager.sceneCount != 2)
        {
            Debug.LogError("Issue switching level");
            return; 
        }
        else
        {
            StartCoroutine(SwapMenu(index));
        }
    }
    public void UnloadMenu()
    {
        //checks whether build index or name was used to add the additive scene and unloads scene based on that
        //resets the value to ensure that additive scene can be loaded
        if (_additiveSceneIndex != -1)
        {
            StartCoroutine(UnloadCurrScene());
            _additiveSceneIndex = -1;
        }
        Time.timeScale = 1;
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
    public int GetStartingLevelIndex()
    {
        return _startingLevelIndex;
    }
    public void SetEnablePauseMenu(bool enable)
    {
        _enablePauseMenu = enable;
    }
    private void ToggleSceneEventSystem(bool enable)
    {
        GameObject EventSystemObj = GameObject.Find("EventSystem");
        if ( EventSystemObj != null)
        {
            EventSystem sceneEventSystem = EventSystemObj.GetComponent<EventSystem>();
            if (sceneEventSystem != null) { sceneEventSystem.enabled = enable; }
        }
    }
    private IEnumerator UnloadCurrScene()
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(_additiveSceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        ToggleSceneEventSystem(true);
    }
    private IEnumerator LoadAsyncScene(int SceneIndex)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Slider slider = FindFirstObjectByType<Slider>();
            if (slider != null)
                slider.value = asyncLoad.progress;
            yield return null;
        }
    }
    private IEnumerator SwapMenu(int index)
    {
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(_additiveSceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        ToggleSceneEventSystem(true);
        LoadMenu(index);
    }
}

//This class is done by Yap Jun Hong Dylan