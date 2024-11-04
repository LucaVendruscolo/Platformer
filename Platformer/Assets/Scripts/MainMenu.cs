using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text motivationalMessageText; // Assign this in the Inspector

    void Start()
    {
        // Display the last motivational message
        motivationalMessageText.text = OpenAIController.lastMotivationalMessage;

        // Unlock cursor for menu navigation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
