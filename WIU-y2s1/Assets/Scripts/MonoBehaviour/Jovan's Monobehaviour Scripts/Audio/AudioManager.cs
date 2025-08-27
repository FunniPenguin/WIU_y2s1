using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;

    [Header("Audio Libraries")]
    [SerializeField] private List<AudioClip> musicClips = new List<AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Music will loop
    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Count)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
    }

    // all audio sources will stop
    public void StopAll()
    {
        musicSource.Stop();
    }
}

// Made By Jovan Yeo Kaisheng