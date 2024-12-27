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
    public void OpenLevel(int levelId){
        string levelName = "Level " + levelId; //when selecting level from level menu, this will
        //remember the level (scene) name
        //SceneManager.LoadScene(levelName); this is before difficulty selection
        levelCanvas.gameObject.SetActive(false); 
        difficultyCanvas.gameObject.SetActive(true);  
        Debug.Log(levelName + " selected");
    }
}
