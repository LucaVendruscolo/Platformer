using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform bulletSpawn;
    public float range = 100f;                
    public int bulletCount = 5;               
    public float spreadAngle = 15f;           

    private PlayerInputActions playerControls;
    private InputAction fire;
    public GameObject explosionPrefab;
    public float explosionDuration = 0.1f;
    public float explosionScale = 0.5f;

    public float knockbackStrength = 5f;      // Strength of the knockback for the shotgun
    private Rigidbody playerRigidbody;        // Reference to the player's Rigidbody for knockback

    void Awake()
    {
        playerControls = new PlayerInputActions();
        playerRigidbody = GetComponentInParent<Rigidbody>();  // Assuming the Weapon is a child of the player
    }

    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        // Apply knockback if the weapon has a spread (indicating it's a shotgun)
        if (spreadAngle > 0 && playerRigidbody != null)
        {
            ApplyKnockback();
        }

        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion spreadRotation = Quaternion.Euler(
                Random.Range(-spreadAngle, spreadAngle),  
                Random.Range(-spreadAngle, spreadAngle),  
                0);                                       

            Vector3 direction = spreadRotation * bulletSpawn.forward;

            if (Physics.Raycast(bulletSpawn.position, direction, out RaycastHit hit, range))
            {
                HandleHit(hit);
                CreateExplosionEffect(hit.point);  // explosion effect.
            }
        }
    }

    private void ApplyKnockback()
    {
        // Calculate the knockback force in the opposite direction of the bullet spawn forward direction
        Vector3 knockbackDirection = -bulletSpawn.forward;
        playerRigidbody.AddForce(knockbackDirection * knockbackStrength, ForceMode.Impulse);
    }

    // apply power up effects
    private void ApplyPowerUp(GameObject powerUpObject)
    {
        PowerUpCollision powerUp = powerUpObject.GetComponent<PowerUpCollision>();

        if (powerUp != null)
        {
            powerUp.ActivatePowerUp();  
            Destroy(powerUpObject);      
        }
    }

    private void CreateExplosionEffect(Vector3 position)
    {
        // creates a little star sprite (for now) that appears for a fraction of a second at whatever u shoot.
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        
        Transform playerTransform = Camera.main.transform;  // this makes the sprite face the player
        explosion.transform.LookAt(playerTransform);  
        
        explosion.transform.localScale *= explosionScale; // makes it smaller.  
        Destroy(explosion, explosionDuration);
    }

    private void HandleHit(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;

        // raycast tag checking
        if (hitObject.CompareTag("enemy"))
        {
            print("Hit " + hitObject.name + "!");
            Destroy(hitObject);  // Destroy the enemy
            BarEventManager.OnSliderReset();
            ScoreEventManager.OnScoreIncrement();
        }
        else if (hitObject.CompareTag("secret"))
        {
            // Trigger the secret event
            SecretTrigger secretTrigger = hitObject.GetComponent<SecretTrigger>();
            if (secretTrigger != null)
            {
                Destroy(hitObject);  // Destroy the secret
                secretTrigger.OnTargetHit();
            }
        }
        else if (hitObject.CompareTag("hitbox"))
        {
            print("Hit " + hitObject.transform.parent.name + "'s hitbox!");
            if (hitObject.transform.parent != null)
            {
                // destroy hitbox parent.
                Destroy(hitObject.transform.parent.gameObject);
            }
            BarEventManager.OnSliderReset();
            ScoreEventManager.OnScoreIncrement();
        }
        else if (hitObject.CompareTag("powerup"))  // power up apply.
        {
            ApplyPowerUp(hitObject);
        }
    }
}
