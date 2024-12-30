using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider bgmSlider;
    private float defaultSensitivity = 25.0f; 
    private float defaultbgm = 0.05f;
    private void Start()
    {
        // Load saved sensitivity or use the default if none exists
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        sensitivitySlider.value = savedSensitivity;
        float savedVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 0.5f);
        bgmSlider.value = savedVolume;

        // Add a listener to save the sensitivity when the slider value changes
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        bgmSlider.onValueChanged.AddListener(OnVolumeChanged);
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
    
    public void SetSliderToDefault()
    {
        sensitivitySlider.value = defaultSensitivity;
    }
    public void SetBGMSliderToDefault()
    {
        bgmSlider.value = defaultbgm;
    }
}
