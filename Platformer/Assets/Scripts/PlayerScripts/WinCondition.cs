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
                GameManager.Instance.SetLastScene(SceneManager.GetActiveScene().name);
                // Mark the level as completed
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
}
