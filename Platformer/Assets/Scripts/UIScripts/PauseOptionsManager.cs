using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider; // Reference to the slider in the pause menu
    public Slider bgmSlider;         // Reference to the background music slider
    public Slider sfxSlider;         // Reference to the sound effects slider

    private float defaultSensitivity = 25.0f; // Default sensitivity value
    private float defaultBGMVolume = 0.05f;   // Default background music volume
    private float defaultSFXVolume = 1f;      // Default sound effects volume

    private PlayerCam playerCam; // Reference to PlayerCam
    private AudioManager audioManager; // Reference to AudioManager

    private void Start()
    {
        // Find the PlayerCam script in the scene
        playerCam = FindObjectOfType<PlayerCam>();

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
        // Update the PlayerCam sensitivity
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
        // Update the background music volume
        if (audioManager != null)
        {
            audioManager.SetVolume("BackgroundMusic", newValue); // Ensure your AudioManager has this method
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
