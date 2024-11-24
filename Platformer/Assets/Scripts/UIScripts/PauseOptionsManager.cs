using UnityEngine;
using UnityEngine.UI;

public class PauseOptionsManager : MonoBehaviour
{
    public Slider sensitivitySlider; // Reference to the slider in the pause menu
    private float defaultSensitivity = 25.0f; // Default sensitivity value
    private PlayerCam playerCam; // Reference to PlayerCam

    private void Start()
    {
        // Find the PlayerCam script in the scene
        playerCam = FindObjectOfType<PlayerCam>();

        // Load the saved sensitivity value or use the default
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        sensitivitySlider.value = savedSensitivity;

        // Add a listener for when the slider value changes
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnDestroy()
    {
        // Remove the listener to avoid memory leaks
        sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
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

    public void SetSliderToDefault()
    {
        sensitivitySlider.value = defaultSensitivity;
        OnSensitivityChanged(defaultSensitivity); // Update PlayerCam and save default value
    }
}
