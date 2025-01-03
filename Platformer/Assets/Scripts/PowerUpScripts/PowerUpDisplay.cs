using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpDisplay : MonoBehaviour
{
    private Text powerUpText;
    public GameObject PowerIcon;
    public GameObject[] PowerIconObjects;
    public Texture JumpIcon;
    public Texture DashIcon;

    private void Awake()
    {
        powerUpText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        PowerUpEventManager.DisplayJumpPowerUp += PowerUpEventManager_DisplayJumpPowerUp;
        PowerUpEventManager.DisplayDashPowerUp += PowerUpEventManager_DisplayDashPowerUp;
        PowerUpEventManager.RemoveDisplayPowerUp += PowerUpEventManager_RemoveDisplayPowerUp;
    }

    private void OnDisable()
    {
        PowerUpEventManager.DisplayJumpPowerUp -= PowerUpEventManager_DisplayJumpPowerUp;
        PowerUpEventManager.DisplayDashPowerUp -= PowerUpEventManager_DisplayDashPowerUp;
        PowerUpEventManager.RemoveDisplayPowerUp -= PowerUpEventManager_RemoveDisplayPowerUp;
    }

    //For now this will only display text, later it will display an image depending on the power up
    private void PowerUpEventManager_DisplayJumpPowerUp()
    {
        powerUpText.text = "Jump";

        //Update UI Text Colour
        if (powerUpText.color != Color.black) {
            powerUpText.color = new Color(32 / 255f, 111 / 255f, 230 / 255f);
        }

        //Update UI Icon
        if (!PowerIcon.activeSelf) {
            foreach (GameObject icon in PowerIconObjects)
            {
                RawImage iconImage = icon.GetComponent<RawImage>();
                iconImage.texture = JumpIcon;
                PowerIcon.gameObject.SetActive(true);
            }
        }
    }

    private void PowerUpEventManager_DisplayDashPowerUp()
    {
        powerUpText.text = "Dash";

        //Update UI Text Colour
        if (powerUpText.color != Color.black)
        {
            powerUpText.color = Color.green;
        }

        //Update UI Icon
        if (!PowerIcon.activeSelf)
        {
            foreach(GameObject icon in PowerIconObjects)
            {
                RawImage iconImage = icon.GetComponent<RawImage>();
                iconImage.texture = DashIcon;
                PowerIcon.gameObject.SetActive(true);
            }
        }

    }

    private void PowerUpEventManager_RemoveDisplayPowerUp()
    {
        powerUpText.text = "No Power";

        //Update the UI to show no power up

        if (powerUpText.color != Color.black)
        {
            powerUpText.color = Color.white;
        }
        if(PowerIcon.gameObject.activeSelf)
        {
            PowerIcon.gameObject.SetActive(false);
        }
    }


}
