using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform bulletSpawn;
    public float range = 100f;      
    public float damage = 20f;          
    public int bulletCount = 5;               
    public float spreadAngle = 15f;           

    private PlayerInputActions playerControls;
    private InputAction fire;
    public GameObject explosionPrefab;
    public float explosionDuration = 0.1f;
    public float explosionScale = 0.5f;

    public float knockbackStrength = 50f;      
    private Rigidbody playerRigidbody;         // for knocking the player back (shotgun)

    public float reloadTime = 1.5f;            
    private bool isReloading = false;         
    public event Action<float> OnReloadStart;

    void Awake()
    {
        playerControls = new PlayerInputActions();
        playerRigidbody = GetComponentInParent<Rigidbody>();  
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
        // if the weapon is currently reloading, return. 
        if (isReloading)
        {
            return;
        }

        // start reload process.
        StartCoroutine(Reload());

        // knockback (only on shotgun)
        if (spreadAngle > 0 && playerRigidbody != null)
        {
            ApplyKnockback();
        }

        for (int i = 0; i < bulletCount; i++) // applies bullets.
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
        isReloading = true; 
        OnReloadStart?.Invoke(reloadTime);
        yield return new WaitForSeconds(reloadTime); 
        isReloading = false;  

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
            // Apply damage to the enemy
            EnemyHealth enemyHealth = hitObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                print($"Hit {hitObject.name}! Dealt {damage} damage.");
            }
            else
            {
                Debug.LogWarning($"{hitObject.name} has no EnemyHealth component.");
            }
        }
        else if (hitObject.CompareTag("hitbox"))
        {
            // Apply damage to the parent enemy via the hitbox
            if (hitObject.transform.parent != null)
            {
                EnemyHealth parentHealth = hitObject.transform.parent.GetComponent<EnemyHealth>();
                if (parentHealth != null)
                {
                    parentHealth.TakeDamage(damage);
                    print($"Hit {hitObject.transform.parent.name}'s hitbox! Dealt {damage} damage.");
                }
                else
                {
                    Debug.LogWarning($"Parent of {hitObject.name} has no EnemyHealth component.");
                }
            }
        }
        else if (hitObject.CompareTag("secret"))
        {
            // Handle secret trigger
            SecretTrigger secretTrigger = hitObject.GetComponent<SecretTrigger>();
            if (secretTrigger != null)
            {
                Destroy(hitObject);
                secretTrigger.OnTargetHit();
            }
        }
        else if (hitObject.CompareTag("powerup"))
        {
            // Handle power-ups
            ApplyPowerUp(hitObject);
        }
    }

}
