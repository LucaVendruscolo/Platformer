using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMusic : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure this GameObject persists across scenes
        //DontDestroyOnLoad(gameObject);

        // Get the AudioSource component located in PlayerCam
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;

        // Load the saved volume from PlayerPrefs or use a default value
        float savedVolume = PlayerPrefs.GetFloat("WinMusicVolume", 0.05f);
        audioSource.volume = Mathf.Clamp(savedVolume, 0f, 1f);
    }

    
    public void SetVolume(float volume)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not initialized.");
            return;
        }

        volume = Mathf.Clamp(volume, 0f, 1f);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("WinMusicVolume", volume);
        PlayerPrefs.Save();
    }
}
