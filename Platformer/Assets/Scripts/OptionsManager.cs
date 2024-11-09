using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider; // Reference to the slider in the options menu
    private float defaultSensitivity = 25.0f; // Default sensitivity if no value is saved

    private void Start()
    {
        // Load saved sensitivity or use the default if none exists
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        sensitivitySlider.value = savedSensitivity;

        // Add a listener to save the sensitivity when the slider value changes
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    // This method is called whenever the slider value changes
    private void OnSensitivityChanged(float newValue)
    {
        PlayerPrefs.SetFloat("Sensitivity", newValue);
        PlayerPrefs.Save(); // Ensure the value is saved
    }
    // Method to reset the slider to the default sensitivity value (25)
    public void SetSliderToDefault()
    {
        sensitivitySlider.value = defaultSensitivity;
    }
}
