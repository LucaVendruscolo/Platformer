using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpDisplay : MonoBehaviour
{
    private Text powerUpText;

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

        //Change the color of the text to blue
        //Only if the color is not the shadow color
        if (powerUpText.color != Color.black) {
            powerUpText.color = new Color(32 / 255f, 111 / 255f, 230 / 255f);
        }

    }

    private void PowerUpEventManager_DisplayDashPowerUp()
    {
        powerUpText.text = "Dash";

        //Change the color of the text to green
        //Only if the color is not the shadow color
        if (powerUpText.color != Color.black)
        {
            powerUpText.color = Color.green;
        }
       
    }

    private void PowerUpEventManager_RemoveDisplayPowerUp()
    {
        powerUpText.text = "No Power";
        //Change the color of the text to white
        //Only if the color is not the shadow color
        if (powerUpText.color != Color.black)
        {
            powerUpText.color = Color.white;
        }
    }


}
