using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneManager : MonoBehaviour
{
    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void TryAgain()
    {
        // Reload the game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Menu");
    }
}
