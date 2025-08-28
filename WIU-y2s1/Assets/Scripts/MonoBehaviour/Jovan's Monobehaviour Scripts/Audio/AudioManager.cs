using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Libraries")]
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] private List<AudioClip> sfxClips = new List<AudioClip>();

    [Header("Volumes")]
    [Range(0f, 1f)][SerializeField] private float masterVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float musicVolume = 1f;
    [Range(0f, 1f)][SerializeField] private float sfxVolume = 1f;

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

        //musicSource.loop = false;

        ApplyVolumes();
    }

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

        int randomBattleIndex;
        do
        {
            randomBattleIndex = Random.Range(4, 6); // 4 or 5
        } while (randomBattleIndex == lastPlayedIndex); // avoid repeating same track

        PlayMusic(randomBattleIndex);
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


    // Volume Properties
    public float MasterVolume
    {
        get { return GetVolume("Master"); }
        set
        {
            masterVolume = Mathf.Clamp01(value);
            SetVolume("Master", value);
        }
    }

    public float MusicVolume
    {
        get { return GetVolume("Music"); }
        set
        {
            musicVolume = Mathf.Clamp01(value);
            SetVolume("Music", value);
        }
    }

    public float SfxVolume
    {
        get { return GetVolume("SFX"); }
        set
        {
            sfxVolume = Mathf.Clamp01(value);
            SetVolume("SFX", value);
        }
    }

    private float GetVolume(string parameterName)
    {
        if (audioMixer.GetFloat(parameterName, out float value))
        {
            return Mathf.Pow(10f, value / 20f); // convert from dB to [0..1]
        }
        return 1f;
    }

    private void SetVolume(string parameterName, float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f; // [0..1] to dB
        audioMixer.SetFloat(parameterName, dB);
    }


    private void OnValidate()
    {
        if (audioMixer == null) return;
        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        MasterVolume = masterVolume;
        MusicVolume = musicVolume;
        SfxVolume = sfxVolume;
    }
}
// Made by Jovan Yeo Kaisheng