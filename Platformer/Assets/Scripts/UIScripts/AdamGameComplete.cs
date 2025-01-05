using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class AdamGameComplete : MonoBehaviour
{
    public TMP_Text finalScoreText; // Assign this in the Inspector
    public TMP_Text finalTimeText;  // Assign in the Inspector
    
    private void Start()
    {

        // Display the final score and time from GameManager
        int finalScore = GameManager.Instance != null ? GameManager.Instance.finalScore : 0;
        float finalTime = GameManager.Instance.finalTime;

        // Format the time using TimeSpan
        TimeSpan timeSpan = TimeSpan.FromSeconds(finalTime);
        finalTimeText.text = "Time: " + timeSpan.ToString(@"mm\:ss\:ff");

        // Display the final score
        finalScoreText.text = "Final Score: " + finalScore;
        Debug.Log($"Displayed data: Score = {finalScore}, Time = {finalTime}");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // Reload the scene stored in GameManager.Instance.lastScene
        if (GameManager.Instance != null && !string.IsNullOrEmpty(GameManager.Instance.lastScene))
        {
            SceneManager.LoadScene(GameManager.Instance.lastScene);
        }
        else
        {
            Debug.LogError("No previous scene stored in GameManager!");
        }
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
