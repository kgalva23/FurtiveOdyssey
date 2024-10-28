using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAudioController : MonoBehaviour
{
    [SerializeField] BombController bombController;  // Reference to the BombController script
    [SerializeField] AudioSource bombAudioSource;   // Audio source for the sound effect

    void Update()
    {
        if(bombController != null)
            // Check if the player is in range by calling the boolean function
            if (bombController.IsPlayerInRange())
            {
                // Play the sound effect if the player is within range
                PlaySound();
            }
    }

    void PlaySound()
    {
        if (!bombAudioSource.isPlaying) // Check if the sound is not already playing
        {
            bombAudioSource.Play();
        }
    }
}
