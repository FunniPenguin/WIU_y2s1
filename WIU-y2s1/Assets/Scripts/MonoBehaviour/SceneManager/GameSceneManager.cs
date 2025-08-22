using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private int loadingSceneIndex = 1;
    private AsyncOperation currOperation = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadSceneAsync("PauseMenu");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Warning: code below can crash and freeze unity
            //SwitchScene("StartScene");
        }
    }
    public void SwitchScene(int sceneIndex)
    {

        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public bool SwitchScene(string sceneName)
    {
        //Todo: Fix a bug that causes unity to freeze and not shutdown
        if (currOperation == null)
            currOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        else
            return false;
        currOperation.allowSceneActivation = false;
        SceneManager.LoadScene(loadingSceneIndex, LoadSceneMode.Additive);
        while (!currOperation.isDone)
        {
            if (currOperation.isDone)
            {
                UnloadAdditiveScene(loadingSceneIndex);
                currOperation.allowSceneActivation = true;
                currOperation = null;
            }
        }
        return true;

    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UnloadAdditiveScene(int SceneIndex)
    {
        SceneManager.UnloadSceneAsync(SceneIndex);
    }
    public void UnloadAdditive(string SceneName)
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
