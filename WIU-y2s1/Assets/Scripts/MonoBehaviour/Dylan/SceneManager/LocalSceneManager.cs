using UnityEngine;

public class LocalSceneManager : MonoBehaviour
{
    public void BackToGame()
    {
        GameSceneManager.Instance.UnloadMenu();
    }
    public void SwapMenu(int index)
    {
        GameSceneManager.Instance.SwitchMenu(index);
    }
    public void SwitchActiveScene(int index)
    {
        GameSceneManager.Instance.LoadLevel(index);
    }
    public void RestartLevel()
    {
        GameSceneManager.Instance.ReloadCurrentScene();
    }
    public void SetPauseEnable(bool enabled)
    {
        GameSceneManager.Instance.SetEnablePauseMenu(enabled);
    }
    public void StartNewGame()
    {
        DataPersistenceManager.Instance.NewGame();
    }
    public void NextLevel(int index)
    {
        DataPersistenceManager.Instance.NewLevel();
        GameSceneManager.Instance.LoadLevel(index);
    }
}
