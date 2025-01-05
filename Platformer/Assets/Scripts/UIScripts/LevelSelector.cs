using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//from Rehope games youtube channel 
public class LevelSelector : MonoBehaviour
{
    public Canvas difficultyCanvas;             
    public Canvas levelCanvas;
    public static string levelName;
    public Button[] levelButtons;


    public static Difficulty selectedDifficulty; 
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    private void Start()
    {
        // Unlock levels based on completion
        UnlockLevels();
    }

    private void UnlockLevels()
    {
        for (int i = 0; i < levelButtons.Length; i++) // Loop through all level buttons
        {
            string levelKey = "Level" + (i + 1) + "_Completed"; // Use "Level1_Completed", "Level2_Completed", etc.

            // Check if the level is unlocked based on PlayerPrefs
            bool isUnlocked = PlayerPrefs.GetInt(levelKey, 0) == 1;

            // Enable or disable the button
            levelButtons[i].interactable = isUnlocked;

            // Debugging: Check which levels are being unlocked
            Debug.Log($"Level {i + 1} unlocked: {isUnlocked}");
        }
    }


    public void OpenLevel(int levelId)
    {
        levelName = "Level" + levelId;

        // Save the level name in GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.currentLevelName = levelName;
            Debug.Log($"[LevelSelector] Saved {levelName} to GameManager");
        }

        levelCanvas.gameObject.SetActive(false); 
        difficultyCanvas.gameObject.SetActive(true);
    }
    public void SelectDifficulty(int difficulty)
    {
        // set difficulty level
        selectedDifficulty = (Difficulty)difficulty;
        
        levelName += selectedDifficulty;
        Debug.Log($"Loading level {levelName}");
        SceneManager.LoadScene(levelName);
    }
    public static void MarkLevelCompleted(string levelName)
    {
        // Mark the current level as completed in PlayerPrefs
        string levelKey = levelName + "_Completed";
        PlayerPrefs.SetInt(levelKey, 1);

        // Unlock the next level
        int currentLevelNumber = GetLevelNumber(levelName); // Extract the current level number
        if (currentLevelNumber > 0) // Ensure the level number is valid
        {
            string nextLevelKey = "Level" + (currentLevelNumber + 1) + "_Completed";
            PlayerPrefs.SetInt(nextLevelKey, 1); // Unlock the next level
        }
        else
        {
            Debug.LogError($"MarkLevelCompleted: Failed to unlock next level for {levelName}");
        }

        PlayerPrefs.Save();
    }

    private static int GetLevelNumber(string levelName)
    {
        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(levelName, @"\d+");
        if (match.Success && int.TryParse(match.Value, out int levelNumber))
        {
            return levelNumber;
        }

        Debug.LogError($"Invalid level name format: {levelName}");
        return 0;
    }


}
