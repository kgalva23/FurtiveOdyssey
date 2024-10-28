using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileLauncher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float projectileSpeed = 10.0f;

    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Helpers")]
    [SerializeField] Transform spawnTransform;

    [Header("Audio")]
    [SerializeField] AudioSource enemyGunAudioSource;
    [Range(-5,-1)]
    [SerializeField] float pitchRange = .2f;

    [Header("Timing")]
    [SerializeField] float fireRate = 1.0f; // Time between shots in seconds

    [SerializeField] EnemySpriteController enemySpriteController;

    private bool isFiring = false;  // Track if firing is active

    void Start()
    {
        // Get the EnemySpriteController component from the parent
        enemySpriteController = GetComponentInParent<EnemySpriteController>();
    }

    void FixedUpdate()
    {
        // Check if the player is detected and start firing projectiles if not already firing
        if (enemySpriteController != null && enemySpriteController.playerDetection() && !isFiring)
        {
            StartCoroutine(FireProjectileAtInterval());
        }

        // Stop firing if the player is no longer detected
        if (enemySpriteController != null && !enemySpriteController.playerDetection() && isFiring)
        {
            StopAllCoroutines();  // Stop firing projectiles when the player is out of range
            isFiring = false;
        }
    }

    // Coroutine to fire the projectile at regular intervals
    IEnumerator FireProjectileAtInterval()
    {
        isFiring = true;  // Set firing to active

        while (enemySpriteController != null && enemySpriteController.playerDetection())
        {
            yield return new WaitForSeconds(fireRate);  // Wait for the defined interval before firing again
            Launch();  // Fire a projectile
        }

        isFiring = false;  // Reset firing state if the player is no longer detected
    }

    // Launch a projectile forward
    public void Launch()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, Quaternion.identity);

        // Get the direction the enemy is facing from EnemySpriteController
        float facingDirection = enemySpriteController.transform.localScale.x > 0 ? 1 : -1;

        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-facingDirection * projectileSpeed, 0);

        enemyGunAudioSource.pitch = Random.Range(5f - pitchRange, 1f - pitchRange);
        enemyGunAudioSource.Play();

        Destroy(newProjectile, 2);  // Destroy projectile after 2 seconds to prevent memory leaks
    }
}
