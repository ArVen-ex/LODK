using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isUnlocked = false; // Starts locked!
    public string nextSceneName = "SampleScene"; //change this to game complete screen

    public StopwatchController leveltimer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player touches the door AND the controller has unlocked it...
        if (isUnlocked && other.CompareTag("Player"))
        {

            if (leveltimer != null)
            {
                leveltimer.StopClockAndSave();
            }
            AudioFXManager.Play("Door");
            SceneManager.LoadScene(nextSceneName);
        }
        else if (!isUnlocked && other.CompareTag("Player"))
        {
            Debug.Log("The door is locked. Find the rest of the items!");
        }
    }
}