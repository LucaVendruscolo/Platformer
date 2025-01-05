using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string lastScene = "";       // Tracks the last scene
    public int finalScore = 0;          // For score
    public float finalTime = 0f;        // For timer
    public string currentLevelName;     // Stores the selected level name

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    public void SaveCompletionData()
    {
        if (!string.IsNullOrEmpty(currentLevelName))
        {
            Debug.Log($"Saving completion data for level: {currentLevelName}");

            // Save the best time for the level and difficulty
            SaveBestTime(currentLevelName, finalTime);

            // Mark the current level as completed
            LevelSelector.MarkLevelCompleted(currentLevelName);
        }
        else
        {
            Debug.LogError("GameManager: currentLevelName is empty or null. Cannot save completion data.");
        }
    }

    private void SaveBestTime(string levelName, float finalTime)
    {
        // Use levelName to make the key unique for each level and difficulty
        string bestTimeKey = $"{levelName}_BestTime"; // e.g., "Level1Easy_BestTime"
        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);

        // Save only if the new time is better
        if (finalTime < bestTime)
        {
            Debug.Log($"New best time for {levelName}: {finalTime}");
            PlayerPrefs.SetFloat(bestTimeKey, finalTime);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log($"No new best time for {levelName}. Current best: {bestTime}, Final time: {finalTime}");
        }
    }

    public void SetCurrentLevelName(string levelName)
    {
        currentLevelName = levelName;
        Debug.Log($"GameManager: Current level name set to {currentLevelName}");
    }

    public float GetBestTime(string levelName, string difficulty)
    {
        string bestTimeKey = $"{levelName}{difficulty}_BestTime"; // e.g., "Level1Easy_BestTime"
        return PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue); // Return best time or float.MaxValue if none exists
    }
}
