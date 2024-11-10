
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
        Debug.Log(other.gameObject);
        Debug.Log(other.gameObject.tag);
        //Only check for player and bullet collision
        //On bullet collisions, destroy the bullet
        if (other.gameObject.tag == "bullet") Destroy(other.gameObject);
        //If the object is not a player, return
        else if (other.gameObject.tag != "player") return;

        if (powerType == PowerUpType.Jump) {
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

        //Destroy the power up
        Destroy(gameObject);



    }

}
