using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialPopupManager : MonoBehaviour
{
    public GameObject popupPrefab; // Reference to the pop-up prefab
    private GameObject currentPopup; // Holds the currently active pop-up

    public void ShowPopup(string title, string message)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // If a pop-up is already active, destroy it
        if (currentPopup != null)
        {
            Destroy(currentPopup);
        }

        // Instantiate a new pop-up
        currentPopup = Instantiate(popupPrefab, transform);

        // Set title and message text
        TextMeshProUGUI titleText = currentPopup.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI messageText = currentPopup.transform.Find("Message").GetComponent<TextMeshProUGUI>();

        Button okButton = currentPopup.transform.Find("OKButton").GetComponent<Button>();

        if (titleText != null) titleText.text = title;
        if (messageText != null) messageText.text = message;

        // Add a listener to destroy the current pop-up when "Got It!" is clicked
        okButton.onClick.RemoveAllListeners(); // Ensure no duplicate listeners
        okButton.onClick.AddListener(ClosePopup);
        Time.timeScale = 0f; // Freeze game time
    }

    public void ClosePopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup); // Destroy the current pop-up
            currentPopup = null;

        }
        Time.timeScale = 1f; // Resume game time
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
