using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePushButton : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    [SerializeField] GameObject sawblade;  // Reference to the sawblade GameObject

    [SerializeField] float segmentSpawnDelay = 0.2f;  // Delay between each segment spawn
    [SerializeField] int totalSegments = 10;

    [SerializeField] float sawbladeSpeed = 0.35f; // Speed at which the sawblade moves

    public bool buttonPressed = false;

    public bool spawningBridge = false;  // Flag to control scaling logic

    public bool sawbladeMoving = false;  // Flag to control sawblade movement
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
         // If the sawblade is allowed to move, make it move back and forth
        if (sawbladeMoving)
        {
            MoveSawblade();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        //if Red button interacts with Ross, add 1 to score & destroy Coin
        if (collision.gameObject.name == "Ross" && !buttonPressed) {
            Debug.Log("Red Button Pressed!");
            //ExtendBridge();
            buttonPressed = true;
            spawningBridge = true;
            StartCoroutine(ExtendBridge());
        }
    }

    // Coroutine to extend the bridge by spawning segments
    IEnumerator ExtendBridge()
    {
        Vector3 segmentPosition = bridge.transform.position;  // Starting position of the first segment

        for (int i = 0; i < totalSegments; i++)
        {
            // Instantiate each segment at the next position
            Instantiate(bridge, segmentPosition, Quaternion.identity);

            // Move the segmentPosition for the next one
            segmentPosition += new Vector3(1.0f, 0, 0);  // Adjust this value based on the width of your segment

            yield return new WaitForSeconds(segmentSpawnDelay);  // Add delay between spawning segments
        }

        Debug.Log("Bridge fully extended!");
        spawningBridge = false;
        sawbladeMoving = true;  // Start moving the sawblade
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
