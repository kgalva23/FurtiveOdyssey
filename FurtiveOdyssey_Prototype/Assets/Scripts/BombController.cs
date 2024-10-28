using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombController : MonoBehaviour
{

    [SerializeField] GameObject player;                 // Reference to the player game object
    [SerializeField] GameObject[] enemies;              // Array to hold enemy game objects
    [SerializeField] ParticleSystem BombParticleSystem;    // Particle system for the bomb explosion

    // Threshold distance to trigger the explosion
    [SerializeField] float explosionDistance = 2f;

    public bool playerDestroyed = false; // Flag to check if player is destroyed

    void Start()
    {

    }

    void FixedUpdate()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Check if the player or any enemy is within range
        if (IsPlayerInRange() || IsEnemyInRange())
        {
            // Start the explosion sequence and play the sound
            StartCoroutine(DelayedExplosion());
        }

    }

    // Check if the player is within range of the bomb
    public bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        return distanceToPlayer < explosionDistance;
    }

    // Check if any enemy is within range of the bomb
    public bool IsEnemyInRange()
    {
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < explosionDistance)
            {
                return true; // Return true if any enemy is in range
            }
        }
        return false;
    }

    IEnumerator DelayedExplosion()
    {
        // Wait for 3 seconds before triggering the explosion
        yield return new WaitForSeconds(3);

        // Destroy the player if within range
        if (IsPlayerInRange())
        {
            Destroy(player);
            playerDestroyed = true;  // Mark the player as destroyed
            Debug.Log("Player destroyed by bomb");
        }

        // Destroy enemies if they are within range
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < explosionDistance)
            {
                Destroy(enemy);
                Debug.Log("Enemy destroyed by bomb");
            }
        }

        // Instantiate and play the explosion particle system
        ParticleSystem explosion = Instantiate(BombParticleSystem, transform.position, Quaternion.identity);
        explosion.Play();

        // Destroy the bomb game object
        Destroy(gameObject);
    }

    public bool isPlayerDestroyed() 
    {
        return playerDestroyed;
    }
}
