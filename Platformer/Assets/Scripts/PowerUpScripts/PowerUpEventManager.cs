using UnityEngine.Events;

public class PowerUpEventManager 
{
    public static event UnityAction GiveJumpPowerUp;
    public static event UnityAction UseJumpPowerUp;
    public static event UnityAction GiveDashPowerUp;
    public static event UnityAction UseDashPowerUp;
    public static event UnityAction DisplayJumpPowerUp;
    public static event UnityAction DisplayDashPowerUp;
    public static event UnityAction RemoveDisplayPowerUp;

    //This is to do with giving the player the power up and using it
    public static void OnGiveJumpPowerUp() => GiveJumpPowerUp?.Invoke();
    public static void OnUseJumpPowerUp() => UseJumpPowerUp?.Invoke();
    public static void OnGiveDashPowerUp() => GiveDashPowerUp?.Invoke();
    public static void OnUseDashPowerUp() => UseDashPowerUp?.Invoke();

    //This is to do with displaying the power up
    public static void OnDisplayJumpPowerUp() => DisplayJumpPowerUp?.Invoke();
    public static void OnDisplayDashPowerUp() => DisplayDashPowerUp?.Invoke();
    public static void OnRemoveDisplayPowerUp() => RemoveDisplayPowerUp?.Invoke();

}
