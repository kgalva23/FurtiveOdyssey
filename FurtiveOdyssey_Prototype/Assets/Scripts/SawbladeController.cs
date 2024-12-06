using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeController : MonoBehaviour
{
    [SerializeField] GameObject sawblade;
    [SerializeField] float sawbladeSpeed = 0.35f; // Speed at which the sawblade moves
    public Vector3 sawbladeStartPosition;
    public Vector3 sawbladeEndPosition;
    public float sawbladeMovementTime = 0.0f;  // Timer for the sawblade movement


    // Start is called before the first frame update
    void Start()
    {
        sawbladeStartPosition = sawblade.transform.position;                    // Starting position (front of bridge)
        sawbladeEndPosition = sawbladeStartPosition + new Vector3(9.0f, 0, 0);  // End position (end of bridge)
    }

    // Update is called once per frame
    void Update()
    {
        MoveSawblade();
    }

    // Function to move the sawblade back and forth along the bridge
    void MoveSawblade()
    {
        // Increment the local movement timer
        sawbladeMovementTime += Time.deltaTime;

        // Move the sawblade between the start and end positions
        float pingPongValue = Mathf.PingPong(sawbladeMovementTime * sawbladeSpeed, 1.0f);
        sawblade.transform.position = Vector3.Lerp(sawbladeStartPosition, sawbladeEndPosition, pingPongValue);
    }
}
