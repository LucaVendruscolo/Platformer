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

        // Load the saved volume from PlayerPrefs or use a default value
        float savedVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 0.05f);
        audioSource.volume = savedVolume;
    }

    
    public void SetVolume(float volume)
    {
        audioSource.volume = volume; // Update the AudioSource volume
        PlayerPrefs.SetFloat("BackgroundMusicVolume", volume); // Save the volume to PlayerPrefs
        PlayerPrefs.Save();
    }
}
