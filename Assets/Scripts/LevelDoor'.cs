using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isUnlocked = false; // Starts locked!
    public string nextSceneName = "SampleScene"; //change this to game complete screen

    [Header("UI Settings")]
    public Text lockedText;
    public float msgDisplayTime = 2.4f;


    public StopwatchController leveltimer;


    private void Start()
    {
        if (lockedText != null)
        {
            lockedText.text = "";
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player touches the door AND the controller has unlocked it...
        if (isUnlocked && other.CompareTag("Player"))
        {

            if (leveltimer != null)
            {
                leveltimer.StopClockAndSave(); //make sure to add the stopwatch controller to to the this door script
            }


            AudioFXManager.Play("Door");
            SceneManager.LoadScene(nextSceneName);
        }
        else if (!isUnlocked && other.CompareTag("Player"))
        {
            if (lockedText != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowLockedMessage());
            }
        }
    }

    private IEnumerator ShowLockedMessage()
    {
        lockedText.text = "The door is locked. Find the rest of the items!";
        yield return new WaitForSeconds(msgDisplayTime);
        lockedText.text = "";
    }
}