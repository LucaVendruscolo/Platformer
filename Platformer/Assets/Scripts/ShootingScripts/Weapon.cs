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

    private Animator animator; // recoil animation
    public ParticleSystem muzzleFlash; // muzzle flash effect
    public AudioSource pistolShootingSound; // pistol sound
    public AudioSource shotgunShootingSound; // shotgun sound
    private int selectedGunID; // Moved selectedGunID to a class-level variable

    
    void Awake()
    {
        playerControls = new PlayerInputActions();
        playerRigidbody = GetComponentInParent<Rigidbody>();  
        selectedGunID = SettingsManager.LoadSelectedGun(); // selected gun id
        animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.Play("Idle");
            
        } else{
            Debug.LogError("no animator found.");
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
        // if the weapon is currently reloading, return. 
        
        if (isReloading)
        {
            return;
        }
        if (pistolShootingSound != null && selectedGunID == 0)
        {
            pistolShootingSound.Play();
        } else if (shotgunShootingSound != null && selectedGunID == 1){
            shotgunShootingSound.Play();
        } 
        else {
            Debug.Log("sound effect NOT playing.");
        }

        if (animator != null) 
        {
            animator.SetTrigger("Shoot");
        } else {
            Debug.Log("Animator is null!!!");
        }

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
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
        if (muzzleFlash != null)
        {
            
            ParticleSystem explosionEffect = Instantiate(muzzleFlash, position, Quaternion.identity);


            explosionEffect.Play();


            Destroy(explosionEffect.gameObject, explosionEffect.main.duration);
        }
        else
        {
            Debug.LogError("no particle system.");
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