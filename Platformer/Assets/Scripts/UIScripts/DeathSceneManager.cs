using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneManager : MonoBehaviour
{
    void Start()//enable navigation
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void TryAgain()
    {
        // Reload the scene stored in Death.lastScene
        if (!string.IsNullOrEmpty(Death.lastScene))
        {
            SceneManager.LoadScene(Death.lastScene);
        }
        else
        {
            Debug.LogError("No previous scene stored!");
        }
    }

    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Menu");
    }
}
