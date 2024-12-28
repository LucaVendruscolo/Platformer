using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float baseBarRemoveSpeed = 0.1f; 
    private float barRemoveSpeed; // depletion speed that is changed via difficulty.
    private bool _isRunning;
    private bool hasSpawnedEnergy = false;

    private void OnEnable()
    {
        BarEventManager.SliderReset += BarEventManager_SliderReset;
    }

    private void OnDisable()
    {
        BarEventManager.SliderReset -= BarEventManager_SliderReset;
    }

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        AdjustSpeedForDifficulty(); // adjusts speed based on difficulty.
    }

    void Update()
    {
        // Reduce the player's health each frame
        slider.value -= barRemoveSpeed * Time.deltaTime;
        if (slider.value <= 0)
        {
            // Player is dead
            SceneManager.LoadScene("DeathScene");
            Debug.Log("Player is dead");
        }
        else if (slider.value <= 0.3 && !hasSpawnedEnergy)
        {
            Debug.Log("Player is low on health");
            // Spawns energy
            BarEventManager.OnSliderSpawnEnergy();
            hasSpawnedEnergy = true;
        }
        else if (slider.value > 0.3)
        {
            hasSpawnedEnergy = false;
        }

        //Adjust Post Processing effects based on health
        PostProcessingManager.OnPostProcessingUpdate(slider.value);
    }


    // Reset the player's health
    private void BarEventManager_SliderReset() => slider.value = slider.maxValue;

    // change health depletion speed basedon difficulty.
    private void AdjustSpeedForDifficulty()
    {
        switch (LevelSelector.selectedDifficulty)
        {
            case LevelSelector.Difficulty.Easy:
                barRemoveSpeed = baseBarRemoveSpeed * 0.5f; // easy speed.
                break;
            case LevelSelector.Difficulty.Medium:
                barRemoveSpeed = baseBarRemoveSpeed; // normal speed.
                break;
            case LevelSelector.Difficulty.Hard:
                barRemoveSpeed = baseBarRemoveSpeed * 1.5f; // hard speed.
                break;
            default:
                Debug.LogWarning("invalid difficulty level, selecting medium health depletion speed.");
                barRemoveSpeed = baseBarRemoveSpeed;
                break;
        }
        Debug.Log($"Health depletion speed set to {barRemoveSpeed} based on difficulty {LevelSelector.selectedDifficulty}");
    }
}