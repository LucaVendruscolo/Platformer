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
    private Rigidbody playerRigidbody; // For knocking the player back (shotgun)

    public float reloadTime = 1.5f; // Passed to GunLoader
    private Animator animator; // Recoil animation
    public ParticleSystem muzzleFlash; // Muzzle flash effect
    private int selectedGunID; // Tracks the gun type (0 for pistol, 1 for shotgun)

    private Func<bool> canShootCallback; // Callback to check if reloading is allowed
    private Action startReloadCallback; // Callback to start reload in GunLoader

    // Setter for the selected gun ID
    public void SetSelectedGunID(int gunID)
    {
        selectedGunID = gunID;
    }

    // Setter for reload callbacks from GunLoader
    public void SetReloadCallback(Action startReload, Func<bool> canShoot)
    {
        startReloadCallback = startReload;
        canShootCallback = canShoot;
    }

    void Awake()
    {
        playerControls = new PlayerInputActions();
        playerRigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.Play("Idle");
        }
        else
        {
            Debug.LogError("No animator found.");
        }
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
        // Check centralized reload state
        if (canShootCallback != null && !canShootCallback() && selectedGunID == 1)
        {
            Debug.Log("Can't shoot, still reloading.");
            return;
        }

        // Play appropriate shooting sound
        if (selectedGunID == 0)
        {
            FindAnyObjectByType<AudioManager>().Play("PistolShootSound");
        }
        else if (selectedGunID == 1)
        {
            FindAnyObjectByType<AudioManager>().Play("ShotgunShootSound");
        }
        else
        {
            Debug.Log("Sound effect NOT playing.");
        }

        // Trigger shooting animation
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }
        else
        {
            Debug.Log("Animator is null!!!");
        }

        // Play muzzle flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Start reload via GunLoader
        if (startReloadCallback != null)
        {
            startReloadCallback();
        }

        // Apply knockback for shotgun
        if (spreadAngle > 0 && playerRigidbody != null)
        {
            ApplyKnockback();
        }

        // handle shooting spread and raycasts.
        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion spreadRotation = Quaternion.Euler(
                UnityEngine.Random.Range(-spreadAngle, spreadAngle),
                UnityEngine.Random.Range(-spreadAngle, spreadAngle),
                0);

            Vector3 direction = spreadRotation * bulletSpawn.forward;
            Vector3 currentPosition = bulletSpawn.position;
            float remainingRange = range;
            bool hitSomething = false;

            while (!hitSomething && remainingRange > 0f)
            {
                if (Physics.Raycast(currentPosition, direction, out RaycastHit hit, remainingRange))
                {
                    if (hit.collider.CompareTag("water"))
                    {
                        remainingRange -= hit.distance;
                        currentPosition = hit.point + direction * 0.01f; 
                        continue; 
                    }

                    HandleHit(hit);
                    CreateExplosionEffect(hit.point); // Explosion effect
                    hitSomething = true; 
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void ApplyKnockback()
    {
        Vector3 knockbackDirection = -bulletSpawn.forward;
        playerRigidbody.AddForce(knockbackDirection * knockbackStrength, ForceMode.Impulse);
    }

    private void CreateExplosionEffect(Vector3 position)
    {
        if (muzzleFlash != null)
        {
            ParticleSystem explosionEffect = Instantiate(muzzleFlash, position, Quaternion.identity);
            explosionEffect.Play();
            Destroy(explosionEffect.gameObject, explosionEffect.main.duration);
        }
        else
        {
            Debug.LogError("No particle system assigned.");
        }
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
            FindAnyObjectByType<AudioManager>().Play("MonsterDeath");
        }
        else if (hitObject.CompareTag("secret"))
        {
            SecretTrigger secretTrigger = hitObject.GetComponent<SecretTrigger>();
            if (secretTrigger != null)
            {
                Destroy(hitObject);
                secretTrigger.OnTargetHit();
                FindAnyObjectByType<AudioManager>().Play("MonsterDeath");
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
            FindAnyObjectByType<AudioManager>().Play("MonsterDeath");
        }
        else if (hitObject.CompareTag("powerup"))
        {
            ApplyPowerUp(hitObject);
            FindAnyObjectByType<AudioManager>().Play("MonsterDeath");
        }
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
}
