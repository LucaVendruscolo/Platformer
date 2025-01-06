using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider mmsSlider;
    public Slider mdsSlider;
    public Slider deathSlider;
    public Slider winSlider;
    private float defaultSensitivity = 25.0f; 
    private float defaultbgm = 0.05f;
    private float defaultsfx = 1f;
    private float defaultmms = 0.1f;
    private float defaultmds = 1f;
    private float defaultdeath = 0.1f;
    private float defaultwin = 0.1f;
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
        //mds
        float savedmds = PlayerPrefs.GetFloat("MenuSoundEffectsVolume", defaultmds);
        mdsSlider.value = savedmds;
        //death
        float savedDeath = PlayerPrefs.GetFloat("DeathMusicVolume", defaultdeath);
        deathSlider.value = savedDeath;
        //win
        float savedWin = PlayerPrefs.GetFloat("WinMusicVolume", defaultwin);
        winSlider.value = savedWin;

        // Add a listener to save the sensitivity when the slider value changes
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.AddListener(OnVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        mmsSlider.onValueChanged.AddListener(OnMMSVolumeChanged);
        mdsSlider.onValueChanged.AddListener(OnMDSVolumeChanged);
        deathSlider.onValueChanged.AddListener(OnDeathVolumeChanged);
        winSlider.onValueChanged.AddListener(OnWinVolumeChanged);
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
    private void OnMDSVolumeChanged(float newValue)
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
    private void OnDeathVolumeChanged(float newValue)
    {
        PlayerPrefs.SetFloat("DeathMusicVolume", newValue);
        PlayerPrefs.Save();
    }
    private void OnWinVolumeChanged(float newValue)
    {
        PlayerPrefs.SetFloat("WinMusicVolume", newValue);
        PlayerPrefs.Save();
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
    public void SetMDSSliderToDefault()
    {
        sfxSlider.value = defaultsfx;
    }
    public void SetMMSSliderToDefault()
    {
        mmsSlider.value = defaultmms;
    }
    public void SetDeathSliderToDefault()
    {
        deathSlider.value = defaultdeath;
    }
    public void SetWinSliderToDefault()
    {
        winSlider.value = defaultwin;
    }
}
