using UnityEngine;

public class GunLoader : MonoBehaviour
{
    public GameObject PistolPrefab;
    public GameObject ShotgunPrefab;
    public GameObject explosionPrefab;  // Reference to the explosion prefab
    private GameObject currentGun;  // currently equipped gun.
    private Transform playerCamera;

    void Start()
    {
        playerCamera = Camera.main.transform;  // the player camera.
        LoadGun();
    }
    

    public void LoadGun()
    {
        if (currentGun != null)
        {
            Destroy(currentGun);
        }

        int selectedGunID = SettingsManager.LoadSelectedGun();
        switch (selectedGunID)
        {
            case 0:  // Pistol
                currentGun = Instantiate(PistolPrefab, transform);
                break;
            case 1:  // Shotgun
                currentGun = Instantiate(ShotgunPrefab, transform);
                break;
            default:
                Debug.LogWarning("Unknown gun ID: " + selectedGunID);
                currentGun = Instantiate(PistolPrefab, transform);  // defaults to pistol.
                break;
        }

        var gunFollowCamera = currentGun.GetComponent<GunFollowCamera>();
        if (gunFollowCamera != null)
        {
            gunFollowCamera.cameraTransform = playerCamera;
        }

        var currentWeapon = currentGun.GetComponent<Weapon>();
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
                        Debug.LogError("no reloadprogressbar script found under reloadbar.");
                    }
                }
                else
                {
                    Debug.LogError("reload bar isn't found under uicanvasobject.");
                }
            }
            else
            {
                Debug.LogError("can't find uicanvasobject.");
            }
        }
}

}
