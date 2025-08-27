using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    public AudioSource AmbienceSource;
    public AudioSource UISource;

    [Header("Audio Clips")]
    public AudioClip UI;
    public AudioClip SFX;
    public AudioClip Music;
    public AudioClip Ambience;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
}
// Made By Jovan Yeo Kaisheng