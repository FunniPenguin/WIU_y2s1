using UnityEngine;
using System.Collections.Generic;

public enum AudioChannel
{
    Music,
    SFX,
    UI,
    Ambience
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [System.Serializable]
    public class ChannelData
    {
        public AudioChannel channel;
        public List<AudioSource> sources = new List<AudioSource>();
        [Range(0f, 1f)] public float volume = 1f;
    }

    [Header("Audio Channels")]
    [SerializeField] private List<ChannelData> channels = new List<ChannelData>();

    private Dictionary<AudioChannel, ChannelData> channelLookup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitChannels();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitChannels()
    {
        channelLookup = new Dictionary<AudioChannel, ChannelData>();

        foreach (var ch in channels)
        {
            channelLookup[ch.channel] = ch;

            // Ensure sources have correct volume
            foreach (var src in ch.sources)
                src.volume = ch.volume;
        }
    }

    // Play music
    public void Play(AudioChannel channel, AudioClip clip, bool loop = false, int sourceIndex = 0)
    {
        if (!channelLookup.ContainsKey(channel)) return;
        var ch = channelLookup[channel];
        if (sourceIndex >= ch.sources.Count) return;

        var src = ch.sources[sourceIndex];
        src.loop = loop;
        src.clip = clip;
        src.Play();
    }

    // Play sfx
    public void PlayOneShot(AudioChannel channel, AudioClip clip, float volumeScale = 1f)
    {
        if (!channelLookup.ContainsKey(channel)) return;
        var ch = channelLookup[channel];

        // Pick first source for one-shots
        if (ch.sources.Count > 0)
            ch.sources[0].PlayOneShot(clip, ch.volume * volumeScale);
    }

    // Stop playback
    public void Stop(AudioChannel channel, int sourceIndex = 0)
    {
        if (!channelLookup.ContainsKey(channel)) return;
        var ch = channelLookup[channel];
        if (sourceIndex < ch.sources.Count)
            ch.sources[sourceIndex].Stop();
    }

    // Set volume globally
    public void SetVolume(AudioChannel channel, float value)
    {
        if (!channelLookup.ContainsKey(channel)) return;
        var ch = channelLookup[channel];

        ch.volume = Mathf.Clamp01(value);
        foreach (var src in ch.sources)
            src.volume = ch.volume;
    }

    public float GetVolume(AudioChannel channel)
    {
        return channelLookup.ContainsKey(channel) ? channelLookup[channel].volume : 0f;
    }
}
