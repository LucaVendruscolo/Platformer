using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTrigger : MonoBehaviour
{
    //Explanation on how to use!
    // Insert game objects that you want to become visible or invisible into the array
    // Make them visible in scene if you want them to become invisible when the target is hit
    // Make them invisible in scene if you want them to become visible when the target is hit
    // Attach this script to the secret target object

    public GameObject[] secretLevelObjects;


    //Make the level visible or invisble when the target has been hit!
    public void OnTargetHit()
    {

        for(int i = 0; i < secretLevelObjects.Length; i++)
        {
            if (secretLevelObjects[i].activeSelf)
            {
                secretLevelObjects[i].SetActive(false);
            }
            else if (!secretLevelObjects[i].activeSelf)
            {
                secretLevelObjects[i].SetActive(true);
            }
        }
    } 
}
