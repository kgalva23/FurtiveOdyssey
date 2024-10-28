using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] GameObject projectileLauncher; // Reference to the current ProjectileLauncher
    [SerializeField] GameObject swordPrefab;        // Reference to the Sword prefab or GameObject
    public GameObject currentWeapon;                       // The active weapon (ProjectileLauncher or Sword)
    public bool isUsingProjectileLauncher = true;          // Track which weapon is being used

    [SerializeField] Sprite regularSwordSprite;     // Default sword sprite
    [SerializeField] Sprite attackSwordSprite;      // Attack sword sprite
    public SpriteRenderer swordSpriteRenderer;            // SpriteRenderer for the sword

    public Vector3 originalPosition;                // Store original position of the sword

    [SerializeField] AudioSource swordAudioSource;
    
    void Start()
    {
        // Ensure that the ProjectileLauncher is the current weapon at the start
        currentWeapon = projectileLauncher;
        // Ensure both objects exist but deactivate the sword initially
        swordPrefab.SetActive(false);
        projectileLauncher.SetActive(true);

        // Get the SpriteRenderer component of swordPrefab
        swordSpriteRenderer = swordPrefab.GetComponent<SpriteRenderer>();

        originalPosition = swordPrefab.transform.localPosition;  // Save the initial position
    }

    void Update()
    {
        // Listen for the "E" key press to switch weapons
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon();
        }
    }

    // Method to switch between ProjectileLauncher and Sword
    void SwitchWeapon()
    {
        if (isUsingProjectileLauncher)
        {
            // Switch to the Sword
            projectileLauncher.SetActive(false); // Disable the gun
            swordPrefab.SetActive(true);         // Enable the sword
            currentWeapon = swordPrefab;     // Set the sword as the current weapon
        }
        else
        {
            // Switch back to the ProjectileLauncher
            swordPrefab.SetActive(false);        // Disable the sword
            projectileLauncher.SetActive(true);  // Enable the gun
            currentWeapon = projectileLauncher;  // Set the ProjectileLauncher as the current weapon
        }

        // Toggle the state
        isUsingProjectileLauncher = !isUsingProjectileLauncher;
    }

    // Coroutine to change the sword sprite temporarily
    public IEnumerator TemporarilyChangeSwordSprite()
    {
        if (swordSpriteRenderer != null)
        {
            // Move the sword down by 2 pixels (0.02 units in Unity’s world space)
            swordPrefab.transform.localPosition = originalPosition + new Vector3(0, -0.1f, 0);

            swordSpriteRenderer.sprite = attackSwordSprite;   // Set to attack sprite

            swordAudioSource.Play();

            yield return new WaitForSeconds(1.0f);            // Wait for 1.0 seconds

            // Move the sword back to its original position
            swordPrefab.transform.localPosition = originalPosition;

            swordSpriteRenderer.sprite = regularSwordSprite;  // Revert to regular sprite
        }
    }

    // Method to update the sword's sprite
    public void UpdateSwordSprite(Sprite newSprite)
    {
        if (swordSpriteRenderer != null)
        {
            swordSpriteRenderer.sprite = newSprite;
        }
    }

    public bool returnProjectileLauncher() 
    {
        return isUsingProjectileLauncher;
    }
}
