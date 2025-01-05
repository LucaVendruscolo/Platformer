using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public TMP_Text motivationalMessageText; 
    public Canvas mainMenuCanvas;         
    public Canvas optionsCanvas;             
    public Canvas levelCanvas;
    public Canvas difficultyCanvas;

    void Start()
    {
        // Display the last motivational message
        //motivationalMessageText.text = OpenAIController.lastMotivationalMessage;

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
        SceneManager.LoadScene("Tutorial");
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
    public void ShowLevel_frm_diff(){
        difficultyCanvas.gameObject.SetActive(false);
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
    public void ResetAllProgress()
    {
        // Total number of levels in the game
        int totalLevels = 3; // Adjust based on your game

        // Loop through all levels
        for (int i = 1; i <= totalLevels; i++) // Assuming levels are 1-indexed
        {
            string baseKey = "Level" + i;

            // Clear level completion data
            PlayerPrefs.DeleteKey(baseKey + "_Completed");

            // Clear best time data for all difficulties
            PlayerPrefs.DeleteKey(baseKey + "Easy_BestTime");
            PlayerPrefs.DeleteKey(baseKey + "Medium_BestTime");
            PlayerPrefs.DeleteKey(baseKey + "Hard_BestTime");
        }

        // Unlock only Level 1
        PlayerPrefs.SetInt("Level1_Completed", 1);

        // Save changes to PlayerPrefs
        PlayerPrefs.Save();

        // Debug confirmation
        Debug.Log("Progress has been reset. Only Level 1 is unlocked.");
    }
}
