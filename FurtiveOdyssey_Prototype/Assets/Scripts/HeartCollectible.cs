using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    [SerializeField] HeartAudioController heartAudioController; // Reference to the audio controller

    //public int healthIncrease = 20;     // Amount of health to add
    [SerializeField] float healthIncrease = 0.2f; // Amount of health to add

    void Start()
    {
        // Find the FreezeEnemiesManager in the scene
        heartAudioController = GameObject.FindObjectOfType<HeartAudioController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collects the heart
        if (other.CompareTag("Player"))
        {
            Debug.Log("Health collected. +" + healthIncrease + " health");

            // Get the Ross component from the Player GameObject
            Ross playerRoss = other.GetComponent<Ross>();
            if (playerRoss != null)
            {
                // Check if currentHealth is greater than 80
                if (playerRoss.currentHealth > 0.8f)
                {
                    playerRoss.currentHealth = playerRoss.maxHealth; // Set to maxHealth
                    Debug.Log("Health exceeded 0.8. Set to maxHealth: " + playerRoss.maxHealth);
                }
                else
                {
                    playerRoss.currentHealth += healthIncrease;
                    // Ensure health doesn't exceed maxHealth otherwise
                    playerRoss.currentHealth = Mathf.Clamp(playerRoss.currentHealth, 0, playerRoss.maxHealth);
                    Debug.Log("Player's current health: " + playerRoss.currentHealth);
                }
            }
            else
            {
                Debug.LogWarning("Ross component not found on Player!");
            }

            // Play the heart collection sound or animation
            if (heartAudioController != null)
            {
                heartAudioController.PlaySound();
            }

            // Destroy the heart after collection
            Destroy(gameObject);
        }
    }

}
