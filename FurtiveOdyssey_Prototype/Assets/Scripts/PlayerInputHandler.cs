using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] Ross ross;

    [SerializeField] GameObject boxObject;

    public float horizontalInput;
    public float jumpingInput;

    public float jumpForce = 5.0f;             // Jump force for moving the player up

    [SerializeField] float normalGravityScale = 3.0f;  // The normal gravity scale for when the player is in the air

    public Rigidbody2D rb2D;                // 2D Rigidbody component of Ross

    public Rigidbody2D otherRb;             // The Rigidbody2D of the box (or other object) we're colliding with

    // Reference to the WeaponSwitcher
    [SerializeField] WeaponSwitcher checkWeapon;

    [SerializeField] Sprite attackSwordSprite;

    void Awake() 
    {
        rb2D = ross.GetComponent<Rigidbody2D>();
        otherRb = boxObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;  // Initialize movement vector

        // Horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        // Jumping logic
        jumpingInput = Input.GetAxis("Vertical"); // Detect jump input (e.g., space/up key)

        if (horizontalInput != 0) 
        {
            movement += new Vector3(horizontalInput, 0, 0);

            // Flip the character to face the correct direction
            FlipCharacter(horizontalInput);
        }
        
        if (ross.returnGroundStatus() && (Input.GetKey(KeyCode.Space))) // If player presses jump and is not currently jumping
        {
            rb2D.gravityScale = normalGravityScale;  // Re-enable gravity when jumping
            movement += new Vector3(0, jumpForce, 0);  // Apply jump force
        }

        //check for F to fire and weapon must be projectile launcher!
        if (Input.GetKey(KeyCode.F) && checkWeapon.returnProjectileLauncher()){
            ross.FireGun();
        }

        //check for F to fire and weapon must be projectile launcher!
        if (Input.GetKeyDown(KeyCode.F) && !checkWeapon.returnProjectileLauncher()){
            //checkWeapon.UpdateSwordSprite(attackSwordSprite);
            StartCoroutine(checkWeapon.TemporarilyChangeSwordSprite());
        }

        if(Input.GetKey(KeyCode.R) && checkWeapon.returnProjectileLauncher()){
            ross.GetProjectileLauncher().Reload();
        }

        if (ross != null)
        {
            ross.Movement(movement);
        }


    }

    void FlipCharacter(float horizontalInput)
    {
        // Store original Y and Z scale values (set them to 2 if they're not already)
        float originalYScale = ross.transform.localScale.y;
        float originalZScale = ross.transform.localScale.z;

        // If moving right, face right (positive x scale), if moving left, face left (negative x scale)
        if (horizontalInput > 0)
        {
            ross.transform.localScale = new Vector3(Mathf.Abs(ross.transform.localScale.x), originalYScale, originalZScale);  // Face right
        }
        else if (horizontalInput < 0)
        {
            ross.transform.localScale = new Vector3(-Mathf.Abs(ross.transform.localScale.x), originalYScale, originalZScale); // Face left
        }
    }

    // Public method to get horizontal input
    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    // Public method to get vertical/jump input
    public float GetJumpingInput()
    {
        return jumpingInput;
    }
    
}
