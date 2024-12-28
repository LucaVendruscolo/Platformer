using UnityEngine.Events;

public class PostProcessingManager
{
    public static event UnityAction<float> PostProcessingUpdate;

    public static void OnPostProcessingUpdate(float health) => PostProcessingUpdate?.Invoke(health);
}
