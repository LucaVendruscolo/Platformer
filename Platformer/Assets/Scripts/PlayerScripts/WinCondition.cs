using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public GameObject winUI;

    //Maybe try to reference UI without having it to be a public object
    //Think bout it
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "win")
        {
            // Store the score before loading the WinScene
            if (GameManager.Instance != null)
            {
                GameManager.Instance.finalScore = FindObjectOfType<Score>().GetCurrentScore(); 
                GameManager.Instance.finalTime = FindObjectOfType<Timer>().GetCurrentTime();
                GameManager.Instance.lastScene = SceneManager.GetActiveScene().name; 
            }
            winUI.SetActive(true);
            SceneManager.LoadScene("WinScene"); // loads the menu when you win for now. will be a win screen in the full game.

        }
    }
}
