
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

    private void OnTriggerEnter(Collider other)
    {
        if (powerType == PowerUpType.Jump) {
            PowerUpEventManager.OnGiveJumpPowerUp();
            PowerUpEventManager.OnDisplayJumpPowerUp();
        }
        else if (powerType == PowerUpType.Dash)
        {
            PowerUpEventManager.OnGiveDashPowerUp();
            PowerUpEventManager.OnDisplayDashPowerUp();
        }

        //Destroy the power up
        Destroy(gameObject);



    }

}
