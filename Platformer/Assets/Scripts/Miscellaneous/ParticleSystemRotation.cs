using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleSystemRotation : MonoBehaviour
{

    public Transform cameraTransform;
    public GameObject slideFX;
    public ParticleSystem slidePS;


    // Update is called once per frame
    void Update()
    {

        // Create a forward direction vector that ignores the Y axis rotation of the camera
        Vector3 forwardDirection = cameraTransform.forward;
        forwardDirection.y = 0;
        forwardDirection.Normalize();

        // Update the rotation of the particle system to match the modified forward direction
        slideFX.transform.rotation = Quaternion.LookRotation(forwardDirection, Vector3.up);

        // Access the main module of the particle system
        var mainModule = slidePS.main;

        // Calculate the direction from the particle system to the camera
        Vector3 directionToCamera = (cameraTransform.position - slidePS.transform.position).normalized;

        // Convert the direction to a rotation and set it as the start rotation
        mainModule.startRotation = Mathf.Atan2(directionToCamera.y, directionToCamera.x) - Mathf.PI / 2;
    }
}
