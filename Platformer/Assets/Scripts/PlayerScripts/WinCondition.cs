using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("win"))
        {
            Debug.Log("Player entered win trigger.");

            // Store the score and time in GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.finalScore = FindObjectOfType<Score>().GetCurrentScore();
                GameManager.Instance.finalTime = FindObjectOfType<Timer>().GetCurrentTime();
                GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;

                Debug.Log($"Stored data in GameManager: Score = {GameManager.Instance.finalScore}, Time = {GameManager.Instance.finalTime}, Scene = {GameManager.Instance.lastScene}");

                // Save completion data using GameManager
                GameManager.Instance.SaveCompletionData();
            }
            else
            {
                Debug.LogError("GameManager.Instance is null! Cannot store score and time.");
            }
            SceneManager.LoadScene("WinScene");
        }
    }
}
