using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteController : MonoBehaviour
{
    [SerializeField] GameObject player;  // Reference to the player game object
    [SerializeField] Sprite relaxedSprite;  // Sprite when enemy is relaxed
    [SerializeField] Sprite alertSprite;    // Sprite when enemy is alert

    public SpriteRenderer spriteRenderer;  // Enemy's sprite renderer

    public float moveSpeed = 2f;           // Movement speed towards the player

    public bool isPlayerDetected = false;          // Track which weapon is being used

    public Rigidbody2D enemyRb;

    public static int numOfEnemiesKilled = 0;

    private bool isDead = false;  // Add this flag to track if the enemy is already counted as dead

    void Awake()
    {
        player = GameObject.Find("Ross");                   //find "Ross" GameObject to set as player to find
        spriteRenderer = GetComponent<SpriteRenderer>();    // Get the SpriteRenderer component attached to the enemy (this game object)
        enemyRb = GetComponent<Rigidbody2D>();              // Get the Rigidbody2D component for movement
    }

    void FixedUpdate()
    {
        // Call the method to update the enemy's sprite based on the distance to the player
        UpdateEnemySprite();

        // If the player is detected, move towards the player
        if (isPlayerDetected)
        {
            MoveTowardsPlayer();
        }
    }

    void UpdateEnemySprite()
    {
        // Calculate the distance between the enemy (this object) and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance to player: " + distanceToPlayer);

        // Change the enemy's sprite based on the distance
        if (distanceToPlayer > 7)
        {
            isPlayerDetected = false;
            spriteRenderer.sprite = relaxedSprite;  // Enemy is relaxed
        }
        else
        {
            isPlayerDetected = true;
            spriteRenderer.sprite = alertSprite;  // Enemy is alert
            
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Move the enemy towards the player using Rigidbody2D
        enemyRb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Flip the enemy to face the movement direction
        FlipEnemy(-direction);
    }

    void FlipEnemy(Vector2 direction)
    {
        // If the enemy is moving right (positive X direction), face right (positive localScale.x)
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // If the enemy is moving left (negative X direction), face left (negative localScale.x)
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (isDead)
            return;  // If the enemy is already dead, exit the function

        //If Projectile hits enemy, they die
        if (other.CompareTag("Projectile")) 
        {
            //ScoreCounter.Instance.AddToScore(1);
            Debug.Log("Enemy Destroyed");

            numOfEnemiesKilled++;

            isDead = true;  // Mark the enemy as dead

             // Calculate the direction from the projectile to the enemy
            Vector2 forceDirection = (other.transform.position - transform.position).normalized;

            // Apply a force to the enemy's Rigidbody2D to simulate a physical hit
            Rigidbody2D enemyRb = GetComponent<Rigidbody2D>();
            float forceMagnitude = 300f;  // Adjust this value for the strength of the hit
            enemyRb.AddForce(-forceDirection * forceMagnitude);
            Destroy(gameObject, 1f);
            Destroy(other.gameObject);
        }

        //If Sword hits enemy, they die
        if (other.CompareTag("Sword")) 
        {
            //ScoreCounter.Instance.AddToScore(1);
            Debug.Log("Enemy Destroyed");

            numOfEnemiesKilled++;

            isDead = true;  // Mark the enemy as dead

            Destroy(gameObject, 1f);
            //Destroy(other.gameObject);
        }
    }

    public bool playerDetection() 
    {
        return isPlayerDetected;
    }

    public int getNumOfEnemiesKilled() 
    {
        return numOfEnemiesKilled;
    }

    public static void ResetEnemiesKilled()
    {
        numOfEnemiesKilled = 0;
    }
}
