using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [Header("Power Up Settings")]
    public float jumpPowerUpForce;
    public float dashPowerUpForce;
    public float dashDuration;
    private bool hasJumpPowerUp = false;
    private bool hasDashPowerUp = false;


    [Header("References")]
    private PlayerControls pc;
    private Rigidbody rb;
    private PlayerMovement pm;

    private void Start()
    {
        pc = GetComponent<PlayerControls>();
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
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
        ResetPowers();
        hasJumpPowerUp = true;

    }

    private void PowerUpManager_UseJumpPowerUp()
    {
        Debug.Log("Use Jump Power Up");
        //Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //Apply Jump Force
        rb.AddForce(Vector3.up * jumpPowerUpForce, ForceMode.Impulse);
        hasJumpPowerUp = false;
        PowerUpEventManager.OnRemoveDisplayPowerUp();
    }

    private void PowerUpManager_GiveDashPowerUp()
    {
        Debug.Log("Give Dash Power Up");
        ResetPowers();

        hasDashPowerUp = true;
    }
    Vector3 originalGravity;
    private void PowerUpManager_UseDashPowerUp()
    {


        originalGravity = Physics.gravity;
        Physics.gravity = new Vector3(0, 0, 0);

        //Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //Apply Dash Force
        rb.AddForce(pm.orientation.forward * dashPowerUpForce, ForceMode.Impulse);
        Invoke(nameof(ResetGravity), dashDuration);


        Debug.Log("Use Dash Power Up");
        hasDashPowerUp = false;
        PowerUpEventManager.OnRemoveDisplayPowerUp();
    }

    private void ResetGravity() {
        Physics.gravity = originalGravity;
    }

    private void ResetPowers() {
        hasJumpPowerUp = false;
        hasDashPowerUp = false;
    }

}
