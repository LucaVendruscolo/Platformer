using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//from Rehope games youtube channel 

public class LevelSelector : MonoBehaviour
{
    public void OpenLevel(int levelId){
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
