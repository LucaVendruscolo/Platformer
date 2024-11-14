using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadProgressBar : MonoBehaviour
{
    private Slider reloadSlider;
    public Weapon weapon;                  // Reference to the Weapon script
    private bool isReloading = false;      // Tracks if the reload is active

    private void Awake()
    {
        reloadSlider = gameObject.GetComponent<Slider>();
        reloadSlider.gameObject.SetActive(false);  // Hide the slider initially
    }

    private void OnEnable()
    {
        weapon.OnReloadStart += StartReload;
    }

    private void OnDisable()
    {
        weapon.OnReloadStart -= StartReload;
    }

    private void StartReload(float reloadTime)
    {
        // Show the slider and start the reload progress bar
        reloadSlider.gameObject.SetActive(true);
        StartCoroutine(FillReloadBar(reloadTime));
    }

    private IEnumerator FillReloadBar(float reloadTime)
    {
        float elapsedTime = 0f;
        isReloading = true;
        reloadSlider.value = 0f;  // Start at empty

        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;
            reloadSlider.value = Mathf.Clamp01(elapsedTime / reloadTime);  // Update the slider based on elapsed time
            yield return null;
        }

        // Hide the slider and reset
        isReloading = false;
        reloadSlider.value = 1f;
        reloadSlider.gameObject.SetActive(false);
    }
}
