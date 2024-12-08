using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadProgressBar : MonoBehaviour
{
    private Slider reloadSlider;
    public Weapon weapon; 

    private void Awake()
    {
        Debug.Log("ReloadProgressBar Awake : "+ gameObject.GetComponent<Slider>());
        reloadSlider = gameObject.GetComponent<Slider>();
        if (reloadSlider != null)
        {
            reloadSlider.gameObject.SetActive(false);  
        }
    }

    private void OnEnable()
    {
        if (weapon != null)
        {
            weapon.OnReloadStart += StartReload;
        }
        else
        {
            Debug.LogError("weapon reference null in reloadprogressbar.");
        }
    }

    private void OnDisable()
    {
        if (weapon != null)
        {
            weapon.OnReloadStart -= StartReload;
        }
    }

    private void StartReload(float reloadTime)
    {

        Debug.Log("StartReload called");
        if (reloadSlider != null)
        {
            Debug.Log("StartReload called with reloadTime : " + reloadTime);
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
        reloadSlider.gameObject.SetActive(false); 
    }

    public void SubscribeToWeapon()
    {
        if (weapon != null)
        {
            weapon.OnReloadStart += StartReload;
        }
        else
        {
            Debug.LogError("Weapon reference null in reloadprogressbar.cs");
        }
    }

}
