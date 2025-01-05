using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public string lastScene = "";   // Tracks the last scene 
    public string currentLevelName; // Stores the current level name (e.g., "Level1")
    public float finalTime;         // Stores the player's final time
    public int finalScore;          // Stores the player's final score

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate GameManagers
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    public void MarkLevelCompleted(string levelName)
    {
        // Mark the current level as completed
        PlayerPrefs.SetInt(levelName + "_Completed", 1);
        Debug.Log($"[GameManager] {levelName} marked as completed!");

        // Unlock the next level
        int currentLevelNumber = GetLevelNumber(levelName);
        if (currentLevelNumber > 0)
        {
            string nextLevelKey = "Level" + (currentLevelNumber + 1) + "_Completed";
            PlayerPrefs.SetInt(nextLevelKey, 1);
            Debug.Log($"[GameManager] Unlocked {nextLevelKey}");
        }

        PlayerPrefs.Save();
    }
    public void SaveBestTime(string levelName, string difficulty, float finalTime)
    {
        string bestTimeKey = $"{levelName}{difficulty}_BestTime"; // e.g., "Level1Easy_BestTime"
        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);

        // Save only if the new time is better
        if (finalTime < bestTime)
        {
            PlayerPrefs.SetFloat(bestTimeKey, finalTime);
            PlayerPrefs.Save();
            Debug.Log($"New best time saved for {bestTimeKey}: {finalTime}");
        }
        else
        {
            Debug.Log($"No new best time for {bestTimeKey}. Current best: {bestTime}, Final time: {finalTime}");
        }
        Debug.Log($"[GameManager] Saving best time with key: {levelName}{difficulty}_BestTime");

    }

    private int GetLevelNumber(string levelName)
    {
        // Extract the level number from the name (e.g., "Level1" -> 1)
        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(levelName, @"\d+");
        if (match.Success && int.TryParse(match.Value, out int levelNumber))
        {
            return levelNumber;
        }

        Debug.LogError($"[GameManager] Invalid level name format: {levelName}");
        return 0; // Default to 0 if parsing fails
    }
    public void SetLastScene(string sceneName)
    {
        lastScene = sceneName;
        Debug.Log($"GameManager: Last scene set to {lastScene}");
    }
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Level1_Completed", 1); // Ensure Level 1 is always unlocked
        PlayerPrefs.Save();
        Debug.Log("[GameManager] Progress reset. Level 1 unlocked.");
    }
}
