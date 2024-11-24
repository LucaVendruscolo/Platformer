using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public Camera cam;
    PlayerControls pc;

    private Vector2 mouseInput;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;

    [Header("fovChange")]
    public float fovAdditiveMultiplier;
    public float minFov;
    public float maxFov;
    private float fovMultiplier;

    private bool canProcessInput = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerControls>();

        Cursor.lockState = CursorLockMode.Locked;    
        Cursor.visible = false;

        // Load the sensitivity value from PlayerPrefs
        sensX = PlayerPrefs.GetFloat("Sensitivity", 2.0f); // 2.0f is a default value
        sensY = sensX; // Assuming both axes use the same sensitivity

        // Start the coroutine to delay input processing
        //This prevents the camera from moving whilst the game is loading
        StartCoroutine(DelayInputProcessing());
    }
    public void UpdateSensitivity(float newSensitivity)
    {
        sensX = newSensitivity;
        sensY = newSensitivity;

        // Save to PlayerPrefs for persistence
        PlayerPrefs.SetFloat("Sensitivity", newSensitivity);
        PlayerPrefs.Save();
    }

    private IEnumerator DelayInputProcessing()
    {
        yield return new WaitForSeconds(0.25f); // Adjust the delay duration as needed
        canProcessInput = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!canProcessInput) return;

        // Debug log to trace mouse input values
        mouseInput = pc.look.ReadValue<Vector2>();

        // Ensure mouse input values are within a reasonable range
        if (mouseInput.x > 100 || mouseInput.y > 100 || mouseInput.x < -100 || mouseInput.y < -100)
        {
            return;
        }
        //Get mouse input
        float mouseX = mouseInput.x * Time.deltaTime * sensX;
        float mouseY = mouseInput.y * Time.deltaTime * sensY;

        //if(rb.velocity.magnitude > 12)
        //{
        //    //fovChange();
        //}

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation = Mathf.Repeat(yRotation, 360f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    //Need to lerp the fov values so that they dont suddenly drastically change
    private void fovChange() {
        fovMultiplier = 1 + ((rb.velocity.magnitude - 10f) * fovAdditiveMultiplier);
        float fovValue = 50 * fovMultiplier;
        cam.fieldOfView = Mathf.Clamp(fovValue, minFov, maxFov);
    }

}
