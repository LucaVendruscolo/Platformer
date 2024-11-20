using UnityEngine.Events;

//Manages the health bar
public class BarEventManager 
{
    public static event UnityAction SliderReset;
    public static event UnityAction SliderSpawnEnergy;

    public static void OnSliderReset() => SliderReset?.Invoke();
    public static void OnSliderSpawnEnergy() => SliderSpawnEnergy?.Invoke();
}
