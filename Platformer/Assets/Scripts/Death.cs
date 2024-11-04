using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [Header("Mandatory Values")]
    private Rigidbody rb;
    public float playerHeight;
    public LayerMask whatIsDanger;

    [Header("DEBUG")]
    public TMP_Text debugDeadState;

    private OpenAIController openAIController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        openAIController = FindObjectOfType<OpenAIController>();

        // Check if openAIController was found
        if (openAIController == null)
        {
            Debug.LogError("OpenAIController not found in the scene. Please ensure it is added to a GameObject in the scene.");
        }
    }

    void FixedUpdate()
    {
        // When you touch the danger ground, you die
        if (Physics.Raycast(rb.position, Vector3.down, playerHeight * 0.5f + 0.1f, whatIsDanger))
        {
            if (openAIController != null)
            {
                openAIController.DisplayMotivationalMessage();
            }

            debugDeadState.text = "Dead";
            SceneManager.LoadScene("Menu"); 
        }
    }
}
