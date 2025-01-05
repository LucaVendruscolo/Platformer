using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure this GameObject persists across scenes
        DontDestroyOnLoad(gameObject);

        // Get the AudioSource component located in PlayerCam
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;

        // Load the saved volume from PlayerPrefs or use a default value
        float savedVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 0.05f);
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
        PlayerPrefs.SetFloat("BackgroundMusicVolume", volume);
        PlayerPrefs.Save();
    }
}
