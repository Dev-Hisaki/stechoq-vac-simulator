using UnityEngine.SceneManagement;
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
        Scene currentScene = SceneManager.GetActiveScene();
        int currentSceneid = currentScene.buildIndex;
        if (currentSceneid == 0)
        {
            Play("BGM");
            ChangeBGMVolume(BGMSlider);
        }
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
        string musicCategory = volumeAttribute.category.ToString();
        Sound s = Array.Find(sounds, sound => sound.name == musicCategory);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = volumeAttribute.volume;
    }

    public struct VolumeAttribute
    {
        public Sound.MusicType category;
        public float volume;
    }
}
