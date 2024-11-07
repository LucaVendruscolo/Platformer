using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [Header("Power Up Settings")]
    float jumpPowerUpForce = 20f;
    float dashPowerUpForce = 10f;
    private bool hasJumpPowerUp = false;
    private bool hasDashPowerUp = false;

    private PlayerControls pc;

    private void Start()
    {
        pc = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.usePower.WasPressedThisFrame()) {
            if (hasJumpPowerUp)
            {
                PowerUpEventManager.OnUseJumpPowerUp();
            }
            else if (hasDashPowerUp)
            {
                PowerUpEventManager.OnUseDashPowerUp();
            }
            Debug.Log("Use Power");
        }
    }

    private void OnEnable()
    {
        PowerUpEventManager.GiveJumpPowerUp += PowerUpManager_GiveJumpPowerUp;
        PowerUpEventManager.UseJumpPowerUp += PowerUpManager_UseJumpPowerUp;
        PowerUpEventManager.GiveDashPowerUp += PowerUpManager_GiveDashPowerUp;
        PowerUpEventManager.UseDashPowerUp += PowerUpManager_UseDashPowerUp;
    }

    private void OnDisable()
    {
        PowerUpEventManager.GiveJumpPowerUp -= PowerUpManager_GiveJumpPowerUp;
        PowerUpEventManager.UseJumpPowerUp -= PowerUpManager_UseJumpPowerUp;
        PowerUpEventManager.GiveDashPowerUp -= PowerUpManager_GiveDashPowerUp;
        PowerUpEventManager.UseDashPowerUp -= PowerUpManager_UseDashPowerUp;
    }

    //Will implement this later on, but this should apply a force to the player
    private void PowerUpManager_GiveJumpPowerUp()
    {
        Debug.Log("Give Jump Power Up");
        hasJumpPowerUp = true;
    }

    private void PowerUpManager_UseJumpPowerUp()
    {
        Debug.Log("Use Jump Power Up");
        hasJumpPowerUp = false;
        PowerUpEventManager.OnRemoveDisplayPowerUp();
    }

    private void PowerUpManager_GiveDashPowerUp()
    {
        Debug.Log("Give Dash Power Up");
        hasDashPowerUp = true;
    }

    private void PowerUpManager_UseDashPowerUp()
    {
        Debug.Log("Use Dash Power Up");
        hasDashPowerUp = false;
        PowerUpEventManager.OnRemoveDisplayPowerUp();
    }

}
