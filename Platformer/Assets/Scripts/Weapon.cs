using System;
using System.Collections;
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

    public float knockbackStrength = 50f;      // Strength of the knockback for the shotgun
    private Rigidbody playerRigidbody;         // Reference to the player's Rigidbody for knockback

    public float reloadTime = 1.5f;            // Duration of the reload time in seconds
    private bool isReloading = false;          // Tracks if the weapon is reloading
    public event Action<float> OnReloadStart;

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
        // Check if the weapon is reloading; if so, don't allow firing
        if (isReloading)
        {
            Debug.Log("Reloading...");
            return;
        }

        // Start the reload process after firing
        StartCoroutine(Reload());

        // Apply knockback if the weapon has a spread (indicating it's a shotgun)
        if (spreadAngle > 0 && playerRigidbody != null)
        {
            ApplyKnockback();
        }

        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion spreadRotation = Quaternion.Euler(
                UnityEngine.Random.Range(-spreadAngle, spreadAngle),  
                UnityEngine.Random.Range(-spreadAngle, spreadAngle),  
                0);                                       

            Vector3 direction = spreadRotation * bulletSpawn.forward;

            if (Physics.Raycast(bulletSpawn.position, direction, out RaycastHit hit, range))
            {
                HandleHit(hit);
                CreateExplosionEffect(hit.point);  // explosion effect.
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;  // Set reloading state
        yield return new WaitForSeconds(reloadTime);  // Wait for the reload duration
        isReloading = false;  // Reset reloading state
    }

    private void ApplyKnockback()
    {
        Vector3 knockbackDirection = -bulletSpawn.forward;
        playerRigidbody.AddForce(knockbackDirection * knockbackStrength, ForceMode.Impulse);
    }

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
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        
        Transform playerTransform = Camera.main.transform;
        explosion.transform.LookAt(playerTransform);  
        
        explosion.transform.localScale *= explosionScale;  
        Destroy(explosion, explosionDuration);
    }

    private void HandleHit(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;

        if (hitObject.CompareTag("enemy"))
        {
            print("Hit " + hitObject.name + "!");
            Destroy(hitObject);  
            BarEventManager.OnSliderReset();
            ScoreEventManager.OnScoreIncrement();
        }
        else if (hitObject.CompareTag("secret"))
        {
            SecretTrigger secretTrigger = hitObject.GetComponent<SecretTrigger>();
            if (secretTrigger != null)
            {
                Destroy(hitObject);  
                secretTrigger.OnTargetHit();
            }
        }
        else if (hitObject.CompareTag("hitbox"))
        {
            print("Hit " + hitObject.transform.parent.name + "'s hitbox!");
            if (hitObject.transform.parent != null)
            {
                Destroy(hitObject.transform.parent.gameObject);
            }
            BarEventManager.OnSliderReset();
            ScoreEventManager.OnScoreIncrement();
        }
        else if (hitObject.CompareTag("powerup"))
        {
            ApplyPowerUp(hitObject);
        }
    }
}
