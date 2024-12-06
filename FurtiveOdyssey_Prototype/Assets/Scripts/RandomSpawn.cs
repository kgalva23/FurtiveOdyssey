using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomSpawn : MonoBehaviour
{
    [SerializeField] List <GameObject> fallingObjects;
    [SerializeField] float timerInterval = 0.5f;
    [SerializeField] float maxSpawnX = 100.0f;
    [SerializeField] float minSpawnX = -7.0f;
    [SerializeField] List<float> platformDetectionHeights; // List of heights to detect platforms

    float gameTimer;
    public int counter = 0;

    void FixedUpdate() 
    {
        gameTimer += Time.deltaTime;    //set gameTimer to keep track of 

        //spawn objects every time interval is met, then reset timer
        if (gameTimer >= timerInterval && counter <= 25) 
        {
            SpawnFallingObjectAbovePlatform();
            gameTimer = 0;
            counter++;
        }

    }

     void SpawnFallingObjectAbovePlatform()
    {
        Vector3 spawnPosition = GetRandomSpawnPositionAbovePlatform();

        // If a valid spawn position is found, instantiate the object
        if (spawnPosition != Vector3.zero)
        {
            int randomNum = Random.Range(0, fallingObjects.Count);  // Random object from fallingObjects list
            GameObject objectFall = Instantiate(fallingObjects[randomNum], spawnPosition, Quaternion.identity);

            // Set the initial velocity to zero to allow gravity to handle falling
            objectFall.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    Vector3 GetRandomSpawnPositionAbovePlatform()
    {
        foreach (float detectionHeight in platformDetectionHeights) // Try each detection height
        {
            float randomX = Random.Range(minSpawnX, maxSpawnX);
            Vector2 raycastStart = new Vector2(randomX, detectionHeight);

            // Cast a ray downward to detect platforms
            RaycastHit2D hit = Physics2D.Raycast(raycastStart, Vector2.down, detectionHeight, LayerMask.GetMask("Ground"));

            if (hit.collider != null)
            {
                // Spawn the enemy slightly above the platform
                return new Vector3(randomX, hit.point.y + 1.5f, 0);
            }
        }

        // Return zero vector if no valid position found after multiple attempts
        return Vector3.zero;
    }

}
