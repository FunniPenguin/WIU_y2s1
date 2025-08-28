using UnityEngine;

public class LocalAudioManager : MonoBehaviour
{

    [SerializeField] private int StartMusic;
    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(StartMusic);
        }
    }

    public void PlayBattleMusic()
    {
        AudioManager.Instance.PlayBattleMusic();
    }

    public void PlayAdventureMusic()
    {
        AudioManager.Instance.PlayRandomMusic123();
    }

    public void PlayLoseMusic()
    {
        AudioManager.Instance.PlayLoseMusic();
    }
    public void PlayWinMusic()
    {
        AudioManager.Instance.PlayMusic(7);
    }

    public void PlayBossMusic()
    {
        AudioManager.Instance.PlayMusic(6);
    }
}
// Made by Jovan Yeo Kaisheng