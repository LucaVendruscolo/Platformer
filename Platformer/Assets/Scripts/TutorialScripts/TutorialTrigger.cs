using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public string tutorialTitle = "Tutorial";
    public string tutorialMessage = "Press W to move forward."; // Customize message

    private bool hasShownPopup = false; // To ensure the pop-up only triggers once

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && !hasShownPopup)
        {
            // Find the TutorialPopupManager and display the message
            TutorialPopupManager popupManager = FindObjectOfType<TutorialPopupManager>();
            if (popupManager != null)
            {
                popupManager.ShowPopup(tutorialTitle, tutorialMessage);
                hasShownPopup = true; // Ensure it only triggers once
            }
        }
    }
}
