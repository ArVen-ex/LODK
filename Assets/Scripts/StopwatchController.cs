using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopwatchController : MonoBehaviour
{
    [Header("UI References")]
    public Text currentTimeText;
    public Text bestTimeText;

    [Header("Settings")]
    public bool isDisplayOnly = false; // We will check this box in the Ending Scene!

    private float currentTime;
    private bool isRunning;
    private string levelSaveKey;

    void Start()
    {
        // IF WE ARE IN THE ENDING SCENE:
        if (isDisplayOnly)
        {
            isRunning = false; // Don't start ticking

            // Grab the exact times we temporarily saved right before leaving the level
            float lastTime = PlayerPrefs.GetFloat("EndScreen_LastTime", 0f);
            float bestTime = PlayerPrefs.GetFloat("EndScreen_BestTime", 0f);

            // Display them on the text boxes
            UpdateClockFormat(lastTime, currentTimeText);

            bestTimeText.text = "Best: ";
            UpdateClockFormat(bestTime, bestTimeText);

            return; // Stop the rest of the Start method from running!
        }

        // IF WE ARE IN A NORMAL LEVEL:
        levelSaveKey = SceneManager.GetActiveScene().name + "_FastestTime";
        currentTime = 0f;
        isRunning = true;
        UpdateBestTimeUI();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateClockFormat(currentTime, currentTimeText);
        }
    }

    public void StopClockAndSave()
    {
        isRunning = false;

        float savedFastestTime = PlayerPrefs.GetFloat(levelSaveKey, 9999f);

        // Check for a new record
        if (currentTime < savedFastestTime)
        {
            PlayerPrefs.SetFloat(levelSaveKey, currentTime);
            PlayerPrefs.Save();
            Debug.Log("New Record!");
        }

        // --- NEW: Save the final numbers temporarily just for the Ending Screen ---
        PlayerPrefs.SetFloat("EndScreen_LastTime", currentTime);

        // Grab the best time again (in case they just set a new record)
        float finalBestTime = PlayerPrefs.GetFloat(levelSaveKey, currentTime);
        PlayerPrefs.SetFloat("EndScreen_BestTime", finalBestTime);
        PlayerPrefs.Save();

        UpdateBestTimeUI();
    }

    private void UpdateClockFormat(float timeToDisplay, Text textComponent)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay % 1) * 1000;

        if (textComponent != null)
        {
            textComponent.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }

    private void UpdateBestTimeUI()
    {
        float savedFastestTime = PlayerPrefs.GetFloat(levelSaveKey, 9999f);

        if (bestTimeText != null)
        {
            if (savedFastestTime != 9999f)
            {
                bestTimeText.text = "Best: ";
                UpdateClockFormat(savedFastestTime, bestTimeText);
            }
            else
            {
                bestTimeText.text = "Best: --:--:---";
            }
        }
    }
}