using UnityEngine;

public class PowerUpCollision : MonoBehaviour
{
    private enum PowerUpType
    {
        Jump,
        Dash
    }
    [SerializeField]
    private PowerUpType powerType;

    // activate power up. this now works with raycast
    public void ActivatePowerUp()
    {
        Debug.Log("Power-up activated: " + powerType);

        if (powerType == PowerUpType.Jump) 
        {
            PowerUpEventManager.OnGiveJumpPowerUp();
            PowerUpEventManager.OnDisplayJumpPowerUp();
            BarEventManager.OnSliderReset();
            ScoreEventManager.OnScoreIncrement();
        }
        else if (powerType == PowerUpType.Dash)
        {
            PowerUpEventManager.OnGiveDashPowerUp();
            PowerUpEventManager.OnDisplayDashPowerUp();
            BarEventManager.OnSliderReset();
            ScoreEventManager.OnScoreIncrement();
        }
    }
}
