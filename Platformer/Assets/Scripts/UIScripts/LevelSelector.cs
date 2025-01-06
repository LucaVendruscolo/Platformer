using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons; 
    public Canvas levelCanvas;
    public Canvas difficultyCanvas;
    public Canvas videoCanvas; // Reference to the VideoCanvas
    public Canvas menuCanvas; // Reference to the MenuCanvas

    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public VideoClip tutorialClip, level1Clip, level2Clip, level3Clip;

    public static string levelName; // Tracks the selected level name
    public enum Difficulty { Easy, Medium, Hard }
    public static Difficulty selectedDifficulty;

    private void Start()
    {
        
        UnlockLevels(); 
    }

    private void UnlockLevels()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == 0)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                // For levels 2, 3, etc., check if they've been completed
                string levelKey = "Level" + (i + 1) + "_Completed";
                bool isUnlocked = PlayerPrefs.GetInt(levelKey, 0) == 1;

                levelButtons[i].interactable = isUnlocked;
                Debug.Log($"[LevelSelector] Level {i + 1} unlocked: {isUnlocked}");
            }
        }
    }

    public void OpenLevel(int levelId)
    {
        levelName = "Level" + levelId;
        Debug.Log($"OpenLevel called with levelId = {levelId}, so levelName = {levelName}");
        PlayerPrefs.SetString("CurrentLevelName", levelName); // Save to PlayerPrefs
        PlayerPrefs.Save();
        
        levelCanvas.gameObject.SetActive(false);
        difficultyCanvas.gameObject.SetActive(true);
    }

    public void SelectDifficulty(int difficulty)
    {
        selectedDifficulty = (Difficulty)difficulty;

        VideoClip selectedClip = null;

        switch (levelName)
        {
            case "Level1":
                selectedClip = level1Clip;
                break;
            case "Level2":
                selectedClip = level2Clip;
                break;
            case "Level3":
                selectedClip = level3Clip;
                break;
        }

        PlayIntroVideoThenLoadScene($"{levelName}{selectedDifficulty}", selectedClip);
    }

    private void PlayIntroVideoThenLoadScene(string sceneName, VideoClip clip = null)
    {
        if (clip != null)
        {
            videoPlayer.clip = clip;
        }

        videoCanvas.gameObject.SetActive(true); // Activate VideoCanvas
        videoCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true; // Enable raycast blocking
        menuCanvas.gameObject.SetActive(false); // Deactivate MenuCanvas

        if (videoPlayer != null && videoPlayer.clip != null)
        {
            Debug.Log("[VideoPlayer] Playing video: " + videoPlayer.clip.name);

            videoPlayer.SetDirectAudioMute(0, true); // 0 is the default audio track index
            videoPlayer.Play();

            videoPlayer.loopPointReached += (VideoPlayer vp) =>
            {
                Debug.Log("[VideoPlayer] Video finished playing.");
                videoCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false; // Disable raycast blocking
                videoCanvas.gameObject.SetActive(false); // Deactivate VideoCanvas
                SceneManager.LoadScene(sceneName);
            };
        }
        else
        {
            Debug.LogError("[VideoPlayer] No video clip assigned.");
            SceneManager.LoadScene(sceneName); // Fallback
        }
    }
}
