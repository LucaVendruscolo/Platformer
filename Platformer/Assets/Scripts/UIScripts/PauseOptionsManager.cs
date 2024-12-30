using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider; 
    public Slider bgmSlider;      
    public Slider sfxSlider;      

    private float defaultSensitivity = 25.0f; 
    private float defaultBGMVolume = 0.05f;   
    private float defaultSFXVolume = 1f;      

    private PlayerCam playerCam; 
    private AudioManager audioManager; 
    private AudioSource bgmAudioSource; 

    private void Start()
    {
        // Find the PlayerCam script in the scene
        playerCam = FindObjectOfType<PlayerCam>();

        if (playerCam != null)
        {
            // Get the AudioSource component from PlayerCam
            bgmAudioSource = playerCam.GetComponent<AudioSource>();
        }

        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Load and initialize sensitivity
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        sensitivitySlider.value = savedSensitivity;

        // Load and initialize BGM volume
        float savedBGMVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", defaultBGMVolume);
        bgmSlider.value = savedBGMVolume;

        // Load and initialize SFX volume
        float savedSFXVolume = PlayerPrefs.GetFloat("GlobalSFXVolume", defaultSFXVolume);
        sfxSlider.value = savedSFXVolume;

        // Add listeners for when slider values change
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnDestroy()
    {
        // Remove the listeners to avoid memory leaks
        sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }

    private void OnSensitivityChanged(float newValue)
    {
        if (playerCam != null)
        {
            playerCam.UpdateSensitivity(newValue);
        }

        // Save the sensitivity to PlayerPrefs
        PlayerPrefs.SetFloat("Sensitivity", newValue);
        PlayerPrefs.Save();
    }

    private void OnBGMVolumeChanged(float newValue)
    {
        if (bgmAudioSource != null && bgmAudioSource.isPlaying)
        {
            bgmAudioSource.volume = newValue; // Adjust volume in real time
        }


        // Save the BGM volume to PlayerPrefs
        PlayerPrefs.SetFloat("BackgroundMusicVolume", newValue);
        PlayerPrefs.Save();
    }

    private void OnSFXVolumeChanged(float newValue)
    {
        // Update the sound effects volume
        if (audioManager != null)
        {
            audioManager.UpdateGlobalSFXVolume(newValue); // Ensure your AudioManager has this method
        }

        // Save the SFX volume to PlayerPrefs
        PlayerPrefs.SetFloat("GlobalSFXVolume", newValue);
        PlayerPrefs.Save();
    }

    public void ToDefaultSens()
    {
        sensitivitySlider.value = defaultSensitivity;
        OnSensitivityChanged(defaultSensitivity);
    }
    public void ToDefaultBGM()
    {
        bgmSlider.value = defaultBGMVolume;
        OnBGMVolumeChanged(defaultBGMVolume);
    }
    public void ToDefaultSFX()
    {
        sfxSlider.value = defaultSFXVolume;
        OnSFXVolumeChanged(defaultSFXVolume);
    }
}
