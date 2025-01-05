using System;
using TMPro;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public TMP_Text easyBestTimeText;   // Assign in Inspector
    public TMP_Text mediumBestTimeText; // Assign in Inspector
    public TMP_Text hardBestTimeText;   // Assign in Inspector

    private string currentLevelName;    // The level name from PlayerPrefs

    private void OnEnable()
    {
        // Get the current level name from PlayerPrefs instead of GameManager
        currentLevelName = PlayerPrefs.GetString("CurrentLevelName");
        Debug.Log($"Retrieved Current Level Name from PlayerPrefs: {currentLevelName}");


        // Update the UI with the best times
        UpdateBestTimes();
        Debug.Log($"Updated for level: {currentLevelName}");
    }

    private void UpdateBestTimes()
    {
        // Use the current level name to fetch best times
        string baseKey = currentLevelName; // e.g., "Level1", "Level2"

        // Retrieve and display the best time for Easy difficulty
        float easyBestTime = PlayerPrefs.GetFloat(baseKey + "Easy_BestTime", float.MaxValue);
        easyBestTimeText.text = FormatTime(easyBestTime, "Best Time:");

        // Retrieve and display the best time for Medium difficulty
        float mediumBestTime = PlayerPrefs.GetFloat(baseKey + "Medium_BestTime", float.MaxValue);
        mediumBestTimeText.text = FormatTime(mediumBestTime, "Best Time:");

        // Retrieve and display the best time for Hard difficulty
        float hardBestTime = PlayerPrefs.GetFloat(baseKey + "Hard_BestTime", float.MaxValue);
        hardBestTimeText.text = FormatTime(hardBestTime, "Best Time:");
    }

    private string FormatTime(float time, string prefix)
    {
        // If no time is recorded, display "--:--:--"
        if (time == float.MaxValue)
        {
            return $"{prefix} --:--:--";
        }

        // Format the time into minutes, seconds, and milliseconds
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return $"{prefix} {timeSpan:mm\\:ss\\:ff}";
    }
}
