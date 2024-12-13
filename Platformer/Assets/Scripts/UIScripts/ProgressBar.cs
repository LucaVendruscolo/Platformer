using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float barRemoveSpeed = 1f;
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
    }


    // Reset the player's health
    private void BarEventManager_SliderReset() => slider.value = slider.maxValue;
}