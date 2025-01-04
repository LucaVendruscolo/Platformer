using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//from Rehope games youtube channel 
public class LevelSelector : MonoBehaviour
{
    public Canvas difficultyCanvas;             
    public Canvas levelCanvas;
    public static string levelName;

    public static Difficulty selectedDifficulty; 
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public void OpenLevel(int levelId){
        levelName = "Level" + levelId; //when selecting level from level menu, this will
        //remember the level (scene) name
        //SceneManager.LoadScene(levelName); this is before difficulty selection
        levelCanvas.gameObject.SetActive(false); 
        difficultyCanvas.gameObject.SetActive(true);  
        Debug.Log(levelName + " selected");
    }
    public void SelectDifficulty(int difficulty)
    {
        // set difficulty level
        selectedDifficulty = (Difficulty)difficulty;
        
        levelName += selectedDifficulty;
        Debug.Log($"Loading level {levelName}");
        SceneManager.LoadScene(levelName);
    }
}
