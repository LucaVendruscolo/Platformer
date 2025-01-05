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
            bgmAudioSource = playerCam.GetComponent<AudioSource>();
            if (bgmAudioSource != null)
            {
                bgmAudioSource.ignoreListenerPause = true; // Allow updates while paused
                Debug.Log("[PauseOptionsManager] BGM AudioSource found and configured.");
            }
            else
            {
                Debug.LogError("[PauseOptionsManager] No AudioSource found on PlayerCam!");
            }
        }

        // Load settings
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        bgmSlider.value = PlayerPrefs.GetFloat("BackgroundMusicVolume", defaultBGMVolume);
        sfxSlider.value = PlayerPrefs.GetFloat("GlobalSFXVolume", defaultSFXVolume);

        // Add listeners
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnDestroy()
    {
        // Remove listeners to avoid memory leaks
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
        PlayerPrefs.SetFloat("Sensitivity", newValue);
        PlayerPrefs.Save();
    }

    private void OnBGMVolumeChanged(float newValue)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = newValue; // Real-time volume adjustment
            Debug.Log($"[PauseOptionsManager] BGM volume updated to: {newValue}");
        }
        PlayerPrefs.SetFloat("BackgroundMusicVolume", newValue);
        PlayerPrefs.Save();
    }

    private void OnSFXVolumeChanged(float newValue)
    {
        if (audioManager != null)
        {
            audioManager.UpdateGlobalSFXVolume(newValue); // Ensure your AudioManager has this method
        }
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
