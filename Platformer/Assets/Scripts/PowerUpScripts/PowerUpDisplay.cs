using UnityEngine;
using TMPro;

public class PowerUpDisplay : MonoBehaviour
{
    private TMP_Text powerUpText;

    private void Awake()
    {
        powerUpText = GetComponent<TMP_Text>();
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
    }

    private void PowerUpEventManager_DisplayDashPowerUp()
    {
        powerUpText.text = "Dash";
    }

    private void PowerUpEventManager_RemoveDisplayPowerUp()
    {
        powerUpText.text = "No Power";
    }


}
