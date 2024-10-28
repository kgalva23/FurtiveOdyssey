using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{

    [SerializeField] Sprite buttonUnpressed;  // Sprite when button is unpressed
    [SerializeField] Sprite buttonPressed;    // Sprite when button is pressed

    public SpriteRenderer buttonSprite;     // Button's Sprite Renderer

    public bool isButtonPressed = false;    // Track if button is pressed or not

    public int objectsOnButton = 0;          // Counter to track how many objects are on the button

    void Awake() 
    {
        buttonSprite = GetComponent<SpriteRenderer>();
    }

    // This will be called when any object enters the button's trigger area
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object is above the button (on top)
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            objectsOnButton++; // Increment the counter when an object is pressing the button

            if (!isButtonPressed)
            {
                buttonSprite.sprite = buttonPressed;
                isButtonPressed = true;
            }
        }
    }

    // This will be called when any object exits the button's trigger area
    void OnCollisionExit2D(Collision2D collision)
    {
        // Decrement the counter when an object leaves the button
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            objectsOnButton--;

            // If no objects are pressing the button, reset it to the unpressed state
            if (objectsOnButton <= 0)
            {
                buttonSprite.sprite = buttonUnpressed;
                isButtonPressed = false;
                objectsOnButton = 0; // Ensure the counter doesn't go negative
            }
        }
    }

    public bool returnButtonStatus() 
    {
        return isButtonPressed;
    }

}
