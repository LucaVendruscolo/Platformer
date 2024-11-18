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

    private void OnCollisionEnter(Collision collision)
    {
        // When you touch the danger, you die
        Debug.Log("Collision with: " + collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("whatIsDanger"))
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
