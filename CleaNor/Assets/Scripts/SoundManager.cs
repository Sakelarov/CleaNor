using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                var inst = FindObjectOfType<SoundManager>();
                instance = inst;
            }

            return instance;
        }
    }

    public Sound[] sounds;
    private AudioSource[] allAudioSources;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        var soundManagers = GameObject.FindGameObjectsWithTag("soundManager");
        if (soundManagers.Length > 1)
        {
            Destroy(soundManagers[0]);
        }
        DontDestroyOnLoad(this.gameObject);
        Play("music");
    }

    public AudioSource Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found!");
        }

        s.source.Play();

        return s.source;
    }

    public void VolumeZero(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found!");
        }

        s.source.volume = 0;
    }

    public void VolumeZeroAllSound()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.volume = 0;
        }
    }

    public void PauseAllSounds()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Pause();
        }
    }

    public void ResumeAllSounds()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.UnPause();
        }
    }
}

