using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCircularMovement : MonoBehaviour
{
    [SerializeField] float radius = 2f;         // Radius of the circular motion
    [SerializeField] float rotationSpeed = 1f; // Speed of the counter-clockwise rotation
    public Vector3 centerPosition;            // Center of the circular path
    public float currentAngle = 0f;           // Current angle of rotation

    void Start()
    {
        // Set the center of the circular path to the starting position of the block
        centerPosition = transform.position;
    }

    void Update()
    {
        // Increment the angle based on the rotation speed
        currentAngle += rotationSpeed * Time.deltaTime;

        // Ensure the angle stays within 0 to 360 degrees
        if (currentAngle >= 360f)
            currentAngle -= 360f;

        // Calculate the new position based on the angle
        float x = centerPosition.x + radius * Mathf.Cos(currentAngle);
        float y = centerPosition.y + radius * Mathf.Sin(currentAngle);

        // Update the block's position
        transform.position = new Vector3(x, y, transform.position.z);
    }

}
