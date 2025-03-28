using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] monsterDeathSounds;

    public static AudioManager instance;

    [Range(0f, 1f)]
    public float globalSFXVolume = 1f; 
    [Range(0f, 1f)]
    public float globalMonsterDeathVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize all sounds
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * globalSFXVolume; // Apply global multiplier
            s.source.pitch = s.pitch;
        }
        foreach (Sound s in monsterDeathSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * globalSFXVolume; // Apply global multiplier
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(name == "MonsterDeath")
        {
            s = monsterDeathSounds[UnityEngine.Random.Range(0, monsterDeathSounds.Length)];
        }
        if (s == null)
        {
            Debug.LogWarning($"Sound {name} not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound {name} not found!");
            return;
        }
        s.source.Stop();
    }
    public void SetVolume(string name, float volume)
{
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
        Debug.LogWarning($"Sound {name} not found!");
        return;
    }

    s.source.volume = volume;
}


    // Method to update the global SFX volume
    public void UpdateGlobalSFXVolume(float multiplier)
    {
        globalSFXVolume = multiplier;

        foreach (Sound s in sounds)
        {
            if (s.source != null)
            {
                s.source.volume = s.volume * globalSFXVolume; // Scale individual volume
            }
        }

        PlayerPrefs.SetFloat("GlobalSFXVolume", globalSFXVolume);
        PlayerPrefs.Save();
    }
    public void UpdateGlobalMDSVolume(float multiplier)
    {
        globalMonsterDeathVolume = multiplier;

        foreach (Sound mds in monsterDeathSounds)
        {
            if (mds.source != null)
            {
                mds.source.volume = mds.volume * globalMonsterDeathVolume;
            }
        }

        PlayerPrefs.SetFloat("GlobalMDSVolume", globalMonsterDeathVolume);
        PlayerPrefs.Save();
    }

}
