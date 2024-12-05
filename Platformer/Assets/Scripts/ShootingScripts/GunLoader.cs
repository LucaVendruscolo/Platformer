using UnityEngine;

public class GunLoader : MonoBehaviour
{
    public GameObject PistolPrefab;
    public GameObject ShotgunPrefab;
    public GameObject explosionPrefab;  // Reference to the explosion prefab
    private GameObject currentGun;  // currently equipped gun.
    private Transform playerCamera;

    private int selectedGunID;  // Tracks the currently selected gun (0 for pistol, 1 for shotgun)

    void Start()
    {
        playerCamera = Camera.main.transform;  // the player camera.
        selectedGunID = SettingsManager.LoadSelectedGun(); // Load the initial gun from settings
        LoadGun();
    }

    void Update()
    {
        HandleWeaponSwitch();
    }

    private void HandleWeaponSwitch() // switch using the scroll wheel
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); 

        if (scrollInput > 0f) 
        {
            selectedGunID = (selectedGunID + 1) % 2; 
            LoadGun();
        }
        else if (scrollInput < 0f) 
        {
            selectedGunID = (selectedGunID - 1 + 2) % 2; 
            LoadGun();
        }
    }

    public void LoadGun()
    {
        if (currentGun != null)
        {
            Destroy(currentGun);
        }

        GameObject gunPrefab = null;

        // select default weapon.
        switch (selectedGunID)
        {
            case 0:
                gunPrefab = PistolPrefab;
                break;
            case 1:
                gunPrefab = ShotgunPrefab;
                break;
            default:
                Debug.LogWarning("Unknown gun ID: " + selectedGunID);
                gunPrefab = PistolPrefab; // Default to pistol
                break;
        }

        if (gunPrefab != null)
        {
            // this code is here to stop the gun appearing out of position for a split second.
            currentGun = Instantiate(gunPrefab, transform);
            var currentWeapon = currentGun.GetComponent<Weapon>();
            if (currentWeapon != null)
            {
                currentWeapon.SetSelectedGunID(selectedGunID);
            }
            else
            {
                Debug.LogError("Weapon script not found on the instantiated gun prefab.");
            }
            currentGun.SetActive(false); 

            // positions once. 
            var gunFollowCamera = currentGun.GetComponent<GunFollowCamera>();
            if (gunFollowCamera != null)
            {
                gunFollowCamera.cameraTransform = playerCamera;

                
                gunFollowCamera.transform.rotation = playerCamera.rotation;
                gunFollowCamera.transform.position = playerCamera.position +
                                                    playerCamera.forward * gunFollowCamera.offset.z +
                                                    playerCamera.right * gunFollowCamera.offset.x +
                                                    playerCamera.up * gunFollowCamera.offset.y;
            }

            // then activates the gun.
            currentGun.SetActive(true);

            
            
            if (currentWeapon != null)
            {
                currentWeapon.explosionPrefab = explosionPrefab;

                GameObject canvasObject = GameObject.Find("UICanvasObject");
                if (canvasObject != null)
                {
                    Transform reloadBarTransform = canvasObject.transform.Find("ReloadBar");
                    if (reloadBarTransform != null)
                    {
                        var reloadProgressBar = reloadBarTransform.GetComponent<ReloadProgressBar>();
                        if (reloadProgressBar != null)
                        {
                            reloadProgressBar.weapon = currentWeapon;
                            reloadProgressBar.SubscribeToWeapon();
                        }
                        else
                        {
                            Debug.LogError("No ReloadProgressBar script found under ReloadBar.");
                        }
                    }
                    else
                    {
                        Debug.LogError("ReloadBar isn't found under UICanvasObject.");
                    }
                }
                else
                {
                    Debug.LogError("Can't find UICanvasObject.");
                }
            }
        }
        else
        {
            Debug.LogError("No gun prefab assigned for the selectedGunID: " + selectedGunID);
        }
    }


}
