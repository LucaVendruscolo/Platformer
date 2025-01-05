using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons; // Assign buttons for levels in the Inspector
    public Canvas levelCanvas;
    public Canvas difficultyCanvas;

    public static string levelName; // Tracks the selected level name
    public enum Difficulty { Easy, Medium, Hard }
    public static Difficulty selectedDifficulty;

    private void Start()
    {
        UnlockLevels(); // Unlock levels based on progress
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

        levelCanvas.gameObject.SetActive(false);
        difficultyCanvas.gameObject.SetActive(true);
    }

    public void SelectDifficulty(int difficulty)
    {
        selectedDifficulty = (Difficulty)difficulty;

        // Construct the scene name (e.g., "Level1Easy", "Level2Medium")
        string fullSceneName = levelName + selectedDifficulty;
        Debug.Log($"[LevelSelector] Loading scene: {fullSceneName}");

        SceneManager.LoadScene(fullSceneName);
    }
}
