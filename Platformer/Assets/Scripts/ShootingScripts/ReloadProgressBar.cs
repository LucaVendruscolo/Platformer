using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadProgressBar : MonoBehaviour
{
    private Slider reloadSlider;
    public GunLoader gunLoader; 

    private void Awake()
    {
        reloadSlider = gameObject.GetComponent<Slider>();
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false); // slider is hidden initially.
        }
    }

    private void OnEnable()
    {
        if (gunLoader != null)
        {
            gunLoader.OnReloadStart += StartReload;
        }
        else
        {
            Debug.LogError("GunLoader reference is null in ReloadProgressBar.");
        }
    }

    private void OnDisable()
    {
        if (gunLoader != null)
        {
            gunLoader.OnReloadStart -= StartReload;
        }
    }

    private void StartReload(float reloadTime) // begins filling the bar when reload is called.
    {
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(true);
            StartCoroutine(FillReloadBar(reloadTime));
        }
    }

    private IEnumerator FillReloadBar(float reloadTime)
    {
        float elapsedTime = 0f;
        reloadSlider.value = 0f;

        while (elapsedTime < reloadTime)
        {
            elapsedTime += Time.deltaTime;
            reloadSlider.value = Mathf.Clamp01(elapsedTime / reloadTime);
            yield return null;
        }

        reloadSlider.value = 1f;
        reloadSlider.gameObject.SetActive(false); // slider disappears after filled.
    }

    public void SubscribeToGunLoader()
    {
        if (gunLoader != null)
        {
            gunLoader.OnReloadStart += StartReload;
        }
        else
        {
            Debug.LogError("GunLoader reference is null in ReloadProgressBar.cs");
        }
    }
}
