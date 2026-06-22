using UnityEngine;
using System;

public class DeathZone : MonoBehaviour
{
    // Create an event that other scripts can listen to
    public static event Action OnPlayerDeath;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player touches this specific block...
        if (other.CompareTag("Player"))
        {
            // Fire the death event!
            OnPlayerDeath?.Invoke();
        }
    }


}