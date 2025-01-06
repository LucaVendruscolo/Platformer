using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider bgmSlider;      
    public Slider sfxSlider;      
    public Slider mdsSlider; 

    private float defaultSensitivity = 25.0f;
    private float defaultBGMVolume = 0.05f;
    private float defaultSFXVolume = 1f;
    private float defaultMDSVolume = 1f;

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

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("[PauseOptionsManager] AudioManager instance not found!");
        }

        // Load settings
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        bgmSlider.value = PlayerPrefs.GetFloat("BackgroundMusicVolume", defaultBGMVolume);
        sfxSlider.value = PlayerPrefs.GetFloat("GlobalSFXVolume", defaultSFXVolume);
        mdsSlider.value = PlayerPrefs.GetFloat("GlobalMDSVolume", defaultMDSVolume);

        // Add listeners
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        mdsSlider.onValueChanged.AddListener(OnMDSVolumeChanged); 
    }

    private void OnDestroy()
    {
        // Remove listeners to avoid memory leaks
        sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        mdsSlider.onValueChanged.RemoveListener(OnMDSVolumeChanged); 
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
            audioManager.UpdateGlobalSFXVolume(newValue); 
        }
        PlayerPrefs.SetFloat("GlobalSFXVolume", newValue);
        PlayerPrefs.Save();
    }

    private void OnMDSVolumeChanged(float newValue)
    {
        if (audioManager != null)
        {
            audioManager.UpdateGlobalMDSVolume(newValue); 
        }
        PlayerPrefs.SetFloat("GlobalMDSVolume", newValue);
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

    public void ToDefaultMDS()
    {
        mdsSlider.value = defaultMDSVolume;
        OnMDSVolumeChanged(defaultMDSVolume);
    }
}
