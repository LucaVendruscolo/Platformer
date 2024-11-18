using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign the pause menu UI in the Inspector
    private bool isPaused = false;
    private PlayerInputActions playerControls ;

    void Awake()
    {
        // Initialize the input action asset
        playerControls = new PlayerInputActions();
        // Bind the Pause action
        playerControls.UI.Cancel.performed += ctx => TogglePause();
    }

    void OnEnable()
    {
        playerControls.UI.Enable();
    }

    void OnDisable()
    {
        playerControls.UI.Disable();
    }

    // Toggle the pause state
    private void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false;

        // Lock the cursor back for FPS mode
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable input controls again
        playerControls.UI.Enable();
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Freeze game time
        isPaused = true;

        // Unlock the cursor so player can interact with UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable input controls
        //playerControls.UI.Disable();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale to normal before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        SceneManager.LoadScene("Menu"); 
    }
}
