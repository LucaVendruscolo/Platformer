using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons; 
    public TMP_Text easyBestTimeText;  
    public TMP_Text mediumBestTimeText; 
    public TMP_Text hardBestTimeText;   
    public Canvas levelCanvas;
    public Canvas difficultyCanvas;

    public static string levelName; // Tracks the selected level name
    public enum Difficulty { Easy, Medium, Hard }
    public static Difficulty selectedDifficulty;

    private void Start()
    {
        UnlockLevels(); 
    }

    private void UnlockLevels()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            string levelKey = "Level" + (i + 1) + "_Completed";
            bool isUnlocked = PlayerPrefs.GetInt(levelKey, 0) == 1;

            levelButtons[i].interactable = isUnlocked; // Enable button if level is unlocked
            Debug.Log($"[LevelSelector] Level {i + 1} unlocked: {isUnlocked}");
        }
    }

    public void OpenLevel(int levelId)
    {
        levelName = "Level" + levelId;
        Debug.Log($"[LevelSelector] Selected {levelName}");
        PlayerPrefs.SetString("CurrentLevelName", levelName); // Save to PlayerPrefs
        PlayerPrefs.Save();

        levelCanvas.gameObject.SetActive(false);
        difficultyCanvas.gameObject.SetActive(true);
        UpdateBestTimesForSelectedLevel();
    }

    public void SelectDifficulty(int difficulty)
    {
        selectedDifficulty = (Difficulty)difficulty;

        // Construct the scene name (e.g., "Level1Easy", "Level2Medium")
        string fullSceneName = levelName + selectedDifficulty;
        Debug.Log($"[LevelSelector] Loading scene: {fullSceneName}");

        SceneManager.LoadScene(fullSceneName);
    }
    private void UpdateBestTimesForSelectedLevel()
    {
        Debug.Log($"[LevelSelector] Retrieving best times for {levelName}");

        // Use PlayerPrefs to retrieve best times for the selected level
        float easyBestTime = PlayerPrefs.GetFloat(levelName + "Easy_BestTime", float.MaxValue);
        float mediumBestTime = PlayerPrefs.GetFloat(levelName + "Medium_BestTime", float.MaxValue);
        float hardBestTime = PlayerPrefs.GetFloat(levelName + "Hard_BestTime", float.MaxValue);

        // Update UI for each difficulty
        easyBestTimeText.text = FormatTime(easyBestTime, "Easy Best Time: ");
        mediumBestTimeText.text = FormatTime(mediumBestTime, "Medium Best Time: ");
        hardBestTimeText.text = FormatTime(hardBestTime, "Hard Best Time: ");

        // Debug logs for validation
        Debug.Log($"[LevelSelector] {levelName} Easy Best Time: {easyBestTime}");
        Debug.Log($"[LevelSelector] {levelName} Medium Best Time: {mediumBestTime}");
        Debug.Log($"[LevelSelector] {levelName} Hard Best Time: {hardBestTime}");
    }


    private string FormatTime(float time, string prefix)
    {
        // If no time is recorded, display "--:--:--"
        if (time == float.MaxValue) return $"{prefix} --:--:--";

        // Format the time into minutes, seconds, and milliseconds
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(time);
        return $"{prefix} {timeSpan:mm\\:ss\\:ff}";
    }
}
