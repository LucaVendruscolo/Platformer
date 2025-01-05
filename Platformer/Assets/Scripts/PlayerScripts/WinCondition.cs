using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("win"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.finalScore = FindObjectOfType<Score>().GetCurrentScore(); 
                GameManager.Instance.finalTime = FindObjectOfType<Timer>().GetCurrentTime();
                GameManager.Instance.currentLevelName = SceneManager.GetActiveScene().name;
                GameManager.Instance.SetLastScene(SceneManager.GetActiveScene().name);//for restarting
                // Mark the level as completed
                string difficulty = ExtractDifficulty(GameManager.Instance.currentLevelName);
                string baseLevelName = ExtractBaseLevelName(GameManager.Instance.currentLevelName);
                GameManager.Instance.SaveBestTime(baseLevelName, difficulty, GameManager.Instance.finalTime);
                GameManager.Instance.MarkLevelCompleted(GameManager.Instance.currentLevelName);
            }
            else
            {
                Debug.LogError("[WinCondition] GameManager is null. Cannot save progress.");
            }

            // Load the win screen
            SceneManager.LoadScene("WinScene");
        }
    }
    private string ExtractDifficulty(string sceneName)
    {
        // Assumes the difficulty is at the end of the scene name (e.g., "Level1Easy")
        if (sceneName.EndsWith("Easy")) return "Easy";
        if (sceneName.EndsWith("Medium")) return "Medium";
        if (sceneName.EndsWith("Hard")) return "Hard";
        return ""; // Default if difficulty is not found
    }
    private string ExtractBaseLevelName(string fullLevelName)
    {
        if (fullLevelName.EndsWith("Easy")) return fullLevelName.Replace("Easy", "");
        if (fullLevelName.EndsWith("Medium")) return fullLevelName.Replace("Medium", "");
        if (fullLevelName.EndsWith("Hard")) return fullLevelName.Replace("Hard", "");
        return fullLevelName;
    }
}
