using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider mmsSlider;
    private float defaultSensitivity = 25.0f; 
    private float defaultbgm = 0.05f;
    private float defaultsfx = 1f;
    private float defaultmms = 0.1f;
    private void Start()
    {
        // sensitivity
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        sensitivitySlider.value = savedSensitivity;
        //bgm
        float savedVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", defaultbgm);
        bgmSlider.value = savedVolume;
        //sfx
        float savedsfx = PlayerPrefs.GetFloat("GlobalSFXVolume", defaultsfx);
        sfxSlider.value = savedsfx;
        //mms
        float savedmms = PlayerPrefs.GetFloat("MenuMusicVolume", defaultmms);
        mmsSlider.value = savedmms;

        // Add a listener to save the sensitivity when the slider value changes
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.AddListener(OnVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        mmsSlider.onValueChanged.AddListener(OnMMSVolumeChanged);
    }

    // This method is called whenever the slider value changes
    private void OnSensitivityChanged(float newValue)
    {
        PlayerPrefs.SetFloat("Sensitivity", newValue);
        PlayerPrefs.Save(); // Ensure the value is saved
    }
    private void OnVolumeChanged(float newValue)
    {
        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat("BackgroundMusicVolume", newValue);
        PlayerPrefs.Save();
    }
    private void OnSFXVolumeChanged(float newValue)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.UpdateGlobalSFXVolume(newValue);
        }
    }

    private void OnMMSVolumeChanged(float newValue)
    {
        PlayerPrefs.SetFloat("MenuMusicVolume", newValue);
        PlayerPrefs.Save();

        MainMenuMusic mmsController = FindObjectOfType<MainMenuMusic>();
        if (mmsController != null)
        {
            mmsController.SetVolume(newValue);
        }
    }
    
    public void SetSensSliderToDefault()
    {
        sensitivitySlider.value = defaultSensitivity;
    }
    public void SetBGMSliderToDefault()
    {
        bgmSlider.value = defaultbgm;
    }
    public void SetSFXSliderToDefault()
    {
        sfxSlider.value = defaultsfx;
    }
    public void SetMMSSliderToDefault()
    {
        mmsSlider.value = defaultmms;
    }
}
