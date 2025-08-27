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

    private int lastPlayedIndex = -1;

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

        musicSource.loop = false;
    }

    private void Update()
    {
        // If nothing is playing, keep randomizing between 1–3
        if (!musicSource.isPlaying && musicClips.Count > 0)
        {
            PlayRandomMusic123();
        }
    }

    public void PlayMusic(int index)
    {
        if (index >= 0 && index < musicClips.Count)
        {
            lastPlayedIndex = index;
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
    }

    // Random ONLY between index 1, 2, and 3
    public void PlayRandomMusic123()
    {
        if (musicClips.Count < 4) return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(1, 4); // 1, 2, or 3
        } while (randomIndex == lastPlayedIndex); // avoid repeating same track

        PlayMusic(randomIndex);
    }

    public void StopAll()
    {
        musicSource.Stop();
    }
}
