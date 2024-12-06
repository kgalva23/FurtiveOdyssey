using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAudioController : MonoBehaviour
{
    [SerializeField] AudioSource heartAudioSource;   // Audio source for the sound effect

    public void PlaySound()
    {
        if (heartAudioSource != null && !heartAudioSource.isPlaying) // Play if not already playing
        {
            heartAudioSource.Play();
        }
    }
}
