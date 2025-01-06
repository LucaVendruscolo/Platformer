using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    private AudioSource audioSource1; // Reference to the first AudioSource
    private AudioSource audioSource2; // Reference to the second AudioSource

    private void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length >= 2)
        {
            audioSource1 = audioSources[0];
            audioSource2 = audioSources[1];
        }
        else
        {
            Debug.LogError("[MainMenuMusic] Less than two AudioSources found on the GameObject!");
        }

        // Load the saved volume or use a default value
        float savedVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 0.1f);

        // Apply the saved volume to both AudioSources
        if (audioSource1 != null) audioSource1.volume = savedVolume;
        if (audioSource2 != null) audioSource2.volume = savedVolume;
    }

    // Method to set the volume for both AudioSources and save it
    public void SetVolume(float volume)
    {
        if (audioSource1 != null)
        {
            audioSource1.volume = volume; // Update the volume of the first AudioSource
        }

        if (audioSource2 != null)
        {
            audioSource2.volume = volume; // Update the volume of the second AudioSource
        }

        // Save the volume in PlayerPrefs
        PlayerPrefs.SetFloat("MenuMusicVolume", volume);
        PlayerPrefs.Save();
    }
}
