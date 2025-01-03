using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneSpeedModifier : MonoBehaviour
{
    public float newBaseBarRemoveSpeed = 0.2f; 
    public ProgressBar progressBar;
    private bool isTriggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && !isTriggered)
        {
            isTriggered = true;

            progressBar.baseBarRemoveSpeed = newBaseBarRemoveSpeed;
            progressBar.SendMessage("AdjustSpeedForDifficulty"); 

        }
    }


}
