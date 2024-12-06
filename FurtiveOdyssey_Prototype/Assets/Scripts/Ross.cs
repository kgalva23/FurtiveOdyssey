using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ross : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed = 5;

    [SerializeField] float pushForce = 5f;  // Force applied to push the box

    [SerializeField] AudioSource audioSource;

    [SerializeField] public float maxHealth = 1.0f;

    public float currentHealth;

    public PlayerInputHandler playerInputHandler;

    [SerializeField] ProjectileLauncher projectileLauncher;

    [SerializeField] LevelRecapScript levelRecapScript;

    [SerializeField] GameObject winPanel;  // Reference to the WinPanel

    public float horizontal;
    public float vertical;

    public float damageCooldown = 2f; // Cooldown time in seconds
    public float lastDamageTime = -2f; // Tracks the last time damage was applied

    public bool hasTakenDamage = false;

    public bool isGrounded;
    private int groundCollisionCount = 0;  // Counter for objects touching the ground

    void Awake() 
    {
        LevelTimer.Instance.StartTimer();
        EnemySpriteController.ResetEnemiesKilled();
        rb = GetComponent<Rigidbody2D>();
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();
        currentHealth = maxHealth;  // Initialize currentHealth when the game starts
    }   

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }

    public void Movement(Vector3 movement) 
    {
        rb.velocity = movement * speed;
    }

    void FixedUpdate() 
    {
        // Get inputs from PlayerInputHandler
        horizontal = playerInputHandler.GetHorizontalInput();
        vertical = playerInputHandler.GetJumpingInput();
    }

    void OnTriggerEnter2D(Collider2D other) {
        //if Ross bumps into Coin object, play sound effect
        if (other.gameObject.name.Contains("Coin")){
            audioSource.Play();
        }
        else if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            takeDamage();
        }
        else if (other.gameObject.CompareTag("Sawblade")) //Check if the player collides with a box
        {
            takeDamage();
        }
        else if (other.gameObject.CompareTag("Spike")) //Check if the player collides with a box
        {
            takeDamage();
        }
        else if (other.gameObject.CompareTag("Electricity")) //Check if the player collides with a box
        {
            takeDamage();
        }
        else if (other.gameObject.CompareTag("GoldenBlock")) //Check if the player collides with the Golden block
        {
            winPanel.SetActive(true);               // Set the WinPanel active to display it
            LevelTimer.Instance.StopTimer();
            levelRecapScript.DisplayLevelRecap();
            Debug.Log("Golden Block Triggered by Player");
            UnlockNewLevel();
        }
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with the Ground Layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundCollisionCount++;  // Increase ground collision count
            isGrounded = true;  // Player is grounded
        }

        if (collision.gameObject.CompareTag("Box")) //Check if the player collides with a box
        {
            Debug.Log("Player collided with a box!");

            if (playerInputHandler.otherRb != null) 
            {
                Vector3 pushDirection = new Vector3(horizontal, vertical, 0).normalized;
                playerInputHandler.otherRb.velocity = pushDirection * pushForce;  // Move the box
            }
        }


    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player stops colliding with the Ground Layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundCollisionCount--;         // Decrease ground collision count
            if (groundCollisionCount <= 0)  // Only set isGrounded to false if no ground collisions remain
            {
                isGrounded = false;
            }
        }

        // When we stop colliding with the box, stop moving it
        if (collision.gameObject.CompareTag("Box"))
        {
            //isGrounded = false;
            playerInputHandler.otherRb.velocity = Vector3.zero;  // Set velocity to zero
            //playerInputHandler.otherRb = null;
        }
    }

    //Method for handling particle system collisions
    void OnParticleCollision(GameObject other)
    {
        // Check if the colliding particle system is tagged for damage
        if (other.CompareTag("ElectricityZap"))
        {
            // Check if enough time has passed since the last damage
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                Debug.Log("Particle collision with Electricity detected!");
                takeDamage();
                lastDamageTime = Time.time; // Update the last damage time
            }
            else
            {
                Debug.Log("Damage is on cooldown!");
            }
        }
    }

    public void Recoil(Vector3 amount)
    {
        rb.AddForce(amount, ForceMode2D.Impulse);
    }

    public void FireGun()
    {

        // Check which way Ross is facing by checking the localScale.x value
        float facingDirection = transform.localScale.x > 0 ? 1 : -1;

        // Set the projectile direction based on the facing direction
        Vector2 projectileDirection = new Vector2(facingDirection, 0);  // Horizontal direction only

        // Pass the direction to the projectile launcher and fire the gun
        projectileLauncher.Launch(projectileDirection);
    }

    public void takeDamage() 
    {

        // Check if health is already 0 or less
        if (currentHealth <= 0.0001f)
        {
            LevelTimer.Instance.ResetTimer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;  // Ensure no further code in this method runs
        }
    
        // Otherwise, reduce health
        currentHealth -= 0.2f * maxHealth;
        hasTakenDamage = true;

        // Clamp currentHealth to ensure it doesn't go below zero
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

        Debug.Log("Current Health: " + currentHealth);

        // If health drops to or below 0, reset the level
        if (currentHealth <= 0.0001f)
        {
            LevelTimer.Instance.ResetTimer();
            Debug.Log("Health dropped to 0, resetting level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void UnlockNewLevel() 
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex")) 
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            int newUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1) + 1;
            PlayerPrefs.SetInt("UnlockedLevel", newUnlockedLevel);
            PlayerPrefs.Save();
            Debug.Log("UnlockedLevel updated to: " + newUnlockedLevel);
        }
    }

    public ProjectileLauncher GetProjectileLauncher()
    {
        return projectileLauncher;
    }

    public float currentHealthStatus() 
    {
        return currentHealth;

    }

    public float maxHealthStatus() 
    {
        return maxHealth;

    }

    public bool returnGroundStatus() 
    {
        return isGrounded;
    }
    
}
