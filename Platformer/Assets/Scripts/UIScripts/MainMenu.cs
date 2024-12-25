using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text motivationalMessageText; // Assign this in the Inspector
    public Canvas mainMenuCanvas;            // Reference to the Main Menu canvas
    public Canvas optionsCanvas;             // Reference to the Options canvas
    public Canvas levelCanvas;

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
        mainMenuCanvas.gameObject.SetActive(false); 
        optionsCanvas.gameObject.SetActive(true);   
    }
    public void ShowLevel()
    {
        mainMenuCanvas.gameObject.SetActive(false); 
        levelCanvas.gameObject.SetActive(true);  
    }
    public void ShowMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true); 
        optionsCanvas.gameObject.SetActive(false);  
        optionsCanvas.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
