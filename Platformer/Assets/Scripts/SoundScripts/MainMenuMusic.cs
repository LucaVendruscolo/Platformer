using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource

    private void Awake()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Load the saved volume or use a default value
        float savedVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 0.1f);
        audioSource.volume = savedVolume;
    }

    // Method to set the MMS volume and save it
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume; // Update the AudioSource volume
            PlayerPrefs.SetFloat("MenuMusicVolume", volume); // Save the volume in PlayerPrefs
            PlayerPrefs.Save();
        }
    }
}
