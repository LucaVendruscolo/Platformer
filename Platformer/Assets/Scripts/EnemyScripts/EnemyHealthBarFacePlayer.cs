using UnityEngine;

public class HealthBarFacePlayer : MonoBehaviour
{
    private Transform playerCamera;

    private void Start()
    {
        playerCamera = Camera.main.transform;  // Reference to the main camera
    }

    private void LateUpdate()
    {
        // Rotate the health bar to face the camera
        transform.LookAt(transform.position + playerCamera.forward);
    }
}
