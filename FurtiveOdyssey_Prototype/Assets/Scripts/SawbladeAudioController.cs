using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeAudioController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float maxVolume = 1f; // Maximum volume of the sawblade sound
    public float minVolume = 0.1f; // Minimum volume when player is far away
    public float maxDistance = 10f; // Distance at which sound reaches minimum volume

    [SerializeField] AudioSource sawAudioSource;

    void Update()
    {
        // Calculate the distance between the player and the sawblade
        float distance = Vector2.Distance(player.position, transform.position);

        // Calculate the volume based on the distance
        float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
        
        // Clamp volume to ensure it doesn’t go below minVolume or above maxVolume
        sawAudioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }
}
