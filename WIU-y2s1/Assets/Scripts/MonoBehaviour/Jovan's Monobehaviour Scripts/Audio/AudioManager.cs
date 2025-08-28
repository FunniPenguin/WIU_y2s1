using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Libraries")]
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> sfxClips = new List<AudioClip>();

    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy() { if (this == _instance) { _instance = null; } }

    private int lastPlayedIndex = -1;

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < audioClips.Count)
        {
            lastPlayedIndex = index;
            musicSource.clip = audioClips[index];
            musicSource.Play();
        }
    }

    // Random ONLY between index 1, 2, and 3
    public void PlayRandomMusic123()
    {
        if (audioClips.Count < 4) return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(1, 4); // 1, 2, or 3
        } while (randomIndex == lastPlayedIndex); // avoid repeating same track

        PlayMusic(randomIndex);
    }

    public void PlayBattleMusic()
    {
        if (audioClips.Count < 6) return;

        int randomIndex = Random.Range(4, 6); // 4 or 5
        do
        {
            randomIndex = Random.Range(4, 6); // 4 or 5
        } while (randomIndex == lastPlayedIndex); // avoid repeating same track
    }

    public void PlayLoseMusic()
    {
        if (audioClips.Count > 8)
        {
            PlayMusic(8);
        }
    }



    public void PlaySFX(int index)
    {
        if (index >= 0 && index < sfxClips.Count)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }
    public void StopAll()
    {
        musicSource.Stop();
        sfxSource.Stop();
    }
}
// Made by Jovan Yeo Kaisheng