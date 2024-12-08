using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;
    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        ScoreEventManager.ScoreIncrement += ScoreEventManager_ScoreIncrement;
    }
    private void OnDisable()
    {
        ScoreEventManager.ScoreIncrement -= ScoreEventManager_ScoreIncrement;
    }

    private void ScoreEventManager_ScoreIncrement()
    {
        score+=10;
        scoreText.text = "Score: " + score;
    }
    public void OnGameComplete()
    {
        // Store the final score in GameManager
        GameManager.Instance.finalScore = score;
    }
    public int GetCurrentScore()
    {
        return score;
    }
}
