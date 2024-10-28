using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] buttonController button;
    [SerializeField] float targetHeight = 10f; // Target height for the elevator
    [SerializeField] float duration = 3f;      // Time it takes to raise the elevator in seconds

    public Coroutine elevatorCoroutine = null; // Store reference to the current coroutine

    public bool isElevatorUp = false;         // To track if the elevator is currently raised

    public Vector3 startPosition;             // Starting position of the elevator
    public Vector3 targetPosition;            // Target position based on targetHeight

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;             // Set the starting position of the elevator
        targetPosition = new Vector3(transform.position.x, transform.position.y + targetHeight, transform.position.z); // Set the target position based on the targetHeight
    }

    void FixedUpdate()
    {
        if (button.returnButtonStatus() && !isElevatorUp)
        {
            // Start raising the elevator if the button is pressed and the elevator isn't fully up
            if (elevatorCoroutine != null)
            {
                StopCoroutine(elevatorCoroutine);  // Stop the currently running coroutine
            }
            elevatorCoroutine = StartCoroutine(MoveElevator(targetPosition)); // Start raising the elevator
        }
        else if (!button.returnButtonStatus() && isElevatorUp)
        {
            // Start lowering the elevator if the button is not pressed and the elevator isn't fully down
            if (elevatorCoroutine != null)
            {
                StopCoroutine(elevatorCoroutine);  // Stop the currently running coroutine
            }
            elevatorCoroutine = StartCoroutine(MoveElevator(startPosition)); // Start lowering the elevator
        }

    }

    IEnumerator MoveElevator(Vector3 target)
    {
        isElevatorUp = (target == targetPosition); // Set the direction based on the target

        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position; // Use the current position as the start point

        // Smoothly move the elevator from initialPosition to the target over 'duration' seconds
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the elevator ends at the exact target position
        transform.position = target;

        elevatorCoroutine = null; // Reset coroutine reference
    }
}
