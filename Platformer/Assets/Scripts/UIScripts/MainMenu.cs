using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text motivationalMessageText; // Assign this in the Inspector
    public Canvas mainMenuCanvas;            // Reference to the Main Menu canvas
    public Canvas optionsCanvas;             // Reference to the Options canvas


    void Start()
    {
        // Display the last motivational message
        motivationalMessageText.text = OpenAIController.lastMotivationalMessage;

        // Unlock cursor for menu navigation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Ensure only the main menu is visible at start
        ShowMainMenu();
    }

    public void PlayGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayTutorial()
    { 
        SceneManager.LoadScene("Level Tutorial 1");
    }
    public void ShowOptions()
    {
        mainMenuCanvas.gameObject.SetActive(false); // Hide the Main Menu canvas
        optionsCanvas.gameObject.SetActive(true);   // Show the Options canvas
    }
        // Method to show the Main Menu canvas and hide the Options canvas
    public void ShowMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true);  // Show the Main Menu canvas
        optionsCanvas.gameObject.SetActive(false);  // Hide the Options canvas
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
