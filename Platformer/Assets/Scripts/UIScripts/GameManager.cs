using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string lastScene = "";       // Tracks the last scene
    public int finalScore = 0;          // For score
    public float finalTime = 0f;        // For timer

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }
}
