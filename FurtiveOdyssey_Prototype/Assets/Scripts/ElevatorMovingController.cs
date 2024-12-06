using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovingController : MonoBehaviour
{
    public float speed = 2f; // Movement speed of the platform
    Vector2 leftDirection = Vector2.left;
    Vector2 rightDirection = Vector2.right;

    public Transform player; // Reference to the player object

    public bool moveLeft = false;
    public bool moveRight = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger collider
        if (collision.CompareTag("Player"))
        {
            player = collision.transform; // Cache the player's transform
            UpdateMovementDirection();   // Check the player's position relative to the platform
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        // Continuously update direction while the player is on the platform
        if (collision.CompareTag("Player"))
        {
            UpdateMovementDirection();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Stop movement when the player exits the trigger collider
        if (collision.CompareTag("Player"))
        {
            moveLeft = false;
            moveRight = false;
            player = null; // Clear the player's transform reference
        }
    }

    public void Update()
    {
        // Move the platform based on the direction
        if (moveLeft)
        {
            transform.Translate(leftDirection * speed * Time.deltaTime);
        }
        else if (moveRight)
        {
            transform.Translate(rightDirection * speed * Time.deltaTime);
        }
    }

    public void UpdateMovementDirection()
    {
        // Compare the player's X position to the platform's current X position
        if (player != null)
        {
            if (player.position.x < (transform.position.x + 2))
            {
                moveLeft = true;
                moveRight = false;
            }
            else
            {
                moveRight = true;
                moveLeft = false;
            }
        }
    }
}
