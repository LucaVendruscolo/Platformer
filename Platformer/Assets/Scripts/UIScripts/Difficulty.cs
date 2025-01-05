using System;
using TMPro;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public TMP_Text easyBestTimeText;   // Assign in Inspector
    public TMP_Text mediumBestTimeText; // Assign in Inspector
    public TMP_Text hardBestTimeText;   // Assign in Inspector

    private string currentLevelName; // Store the current level name (e.g., "Level1")

    private void Start()
    {
        // Get the current level name from GameManager
        if (GameManager.Instance != null)
        {
            currentLevelName = GameManager.Instance.currentLevelName;
            Debug.Log($"[DifficultyMenu] Retrieved Current Level Name: {currentLevelName}");
        }
        else
        {
            Debug.LogError("[DifficultyMenu] GameManager.Instance is null! Cannot retrieve level name.");
        }

        // Update the UI with the best times
        UpdateBestTimes();
    }


    private void UpdateBestTimes()
    {
        // Use the current level name to fetch best times
        string baseKey = currentLevelName; // e.g., "Level1", "Level2", "Level3"

        // Retrieve and display the best time for Easy difficulty
        float easyBestTime = PlayerPrefs.GetFloat(baseKey + "Easy_BestTime", float.MaxValue);
        Debug.Log($"Easy Best Time for {baseKey}: {easyBestTime} seconds");
        easyBestTimeText.text = FormatTime(easyBestTime, "Best Time:");

        // Retrieve and display the best time for Medium difficulty
        float mediumBestTime = PlayerPrefs.GetFloat(baseKey + "Medium_BestTime", float.MaxValue);
        Debug.Log($"Medium Best Time for {baseKey}: {mediumBestTime} seconds");
        mediumBestTimeText.text = FormatTime(mediumBestTime, "Best Time:");

        // Retrieve and display the best time for Hard difficulty
        float hardBestTime = PlayerPrefs.GetFloat(baseKey + "Hard_BestTime", float.MaxValue);
        Debug.Log($"Hard Best Time for {baseKey}: {hardBestTime} seconds");
        hardBestTimeText.text = FormatTime(hardBestTime, "Best Time:");
    }
    private string FormatTime(float time, string prefix)
    {
        // If no time is recorded, display "--:--:--"
        if (time == float.MaxValue)
        {
            Debug.Log($"{prefix} No best time recorded for this difficulty.");
            return $"{prefix} --:--:--";
        }

        // Format the time into minutes, seconds, and milliseconds
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string formattedTime = $"{prefix} {timeSpan:mm\\:ss\\:ff}";
        Debug.Log($"{prefix} Formatted Time: {formattedTime}");
        return formattedTime;
    }
}
