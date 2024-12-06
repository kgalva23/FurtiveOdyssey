using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapParticleDamage : MonoBehaviour
{

    void OnParticleCollision(GameObject other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            Ross player = other.GetComponent<Ross>();
            if (player != null)
            {
                player.takeDamage();
                Debug.Log("Player took damage from particles!");
            }
        }
    }
}
