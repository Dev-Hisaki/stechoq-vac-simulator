using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Slider BGMSlider, SFXSlider;
    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
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
        ChangeBGMVolume(BGMSlider);
        Play("BGM");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.Stop();
    }

    public void ChangeBGMVolume(Slider slider)
    {
        if (slider.gameObject.name == "Music Slidebar")
        {
            if (Enum.TryParse("BGM", true, out Sound.MusicType bgm))
            {
                ChangeVolume(new VolumeAttribute { category = bgm, volume = slider.value });
            }
            else
            {
                Debug.LogWarning("Failed to parse MusicType.");
            }
        }
        else
        {
            Debug.LogWarning("This is not a BGM Slider");
        }
    }


    public void ChangeVolume(VolumeAttribute volumeAttribute)
    {

    }

    public struct VolumeAttribute
    {
        public Sound.MusicType category;
        public float volume;
    }
}
