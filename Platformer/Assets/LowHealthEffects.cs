using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LowHealthEffects : MonoBehaviour
{

    public GameObject camera;
    private PostProcessVolume volume;


    private void Start()
    {
        volume = camera.GetComponent<PostProcessVolume>();
    }
    private void OnEnable()
    {
        PostProcessingManager.PostProcessingUpdate += PostProcessingManager_PostProcessingUpdate;
    }

    private void OnDisable()
    {
        PostProcessingManager.PostProcessingUpdate -= PostProcessingManager_PostProcessingUpdate;
    }




    private void PostProcessingManager_PostProcessingUpdate(float health)
    {
        Vignette vignette = volume.profile.GetSetting<Vignette>();
        vignette.intensity.value = 0.4f * (1 - health);
    }
}
