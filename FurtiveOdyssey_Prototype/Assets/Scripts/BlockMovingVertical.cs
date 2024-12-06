using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovingVertical : MonoBehaviour
{
    [SerializeField] float movementAmplitude = 2f;  // Amplitude of the vertical movement
    [SerializeField] float movementSpeed = 1f;      // Speed of the up and down movement
    private Vector3 startPosition;                 // Starting position of the block

    void Start()
    {
        // Save the starting position of the block
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + movementAmplitude * Mathf.Sin(Time.time * movementSpeed);

        // Update the block's position while keeping X and Z constant
        transform.position = new Vector3(startPosition.x, newY, transform.position.z);
    }
}
