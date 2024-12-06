using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PushButton : MonoBehaviour
{

    [SerializeField] GameObject bridge;
    [SerializeField] Tilemap tilemap; // Reference to your Tilemap
    [SerializeField] Vector3Int[] tilesToDestroy; // List of tiles to destroy (positions in grid coordinates)
    [SerializeField] GameObject sawblade;  // Reference to the sawblade GameObject

    [SerializeField] float segmentSpawnDelay = 0.2f;  // Delay between each segment spawn
    [SerializeField] int totalSegments = 5;

    public bool buttonPressed = false;   // Flag to track if button has been pressed or not
    public bool spawningBridge = false;  // Flag to control scaling logic

    void OnCollisionEnter2D(Collision2D collision) 
    {
        //if Red button interacts with Ross, extend bridge and destory top blocks & sawblade
        if (collision.gameObject.name == "Ross" && !buttonPressed) 
        {
            Debug.Log("Red Button Pressed!");
            buttonPressed = true;
            spawningBridge = true;
            StartCoroutine(ExtendBridge());
            DestroySpecificTiles(); // Destroy tiles after button press
            DestroySawblade(); // Destroy the sawblade
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
            segmentPosition += new Vector3(2.0f, 0, 0);  // Adjust this value based on the width of your segment

            yield return new WaitForSeconds(segmentSpawnDelay);  // Add delay between spawning segments
        }

        Debug.Log("Bridge fully extended!");
        spawningBridge = false;
    }

    // Function to destroy specific tiles on the Tilemap
    void DestroySpecificTiles()
    {
        foreach (Vector3Int tilePosition in tilesToDestroy)
        {
            tilemap.SetTile(tilePosition, null); // Remove the tile at the specified grid position
        }
        Debug.Log("Specific tiles destroyed!");
    }

    // Function to destroy the sawblade
    void DestroySawblade()
    {
        if (sawblade != null)
        {
            Destroy(sawblade); // Destroy the sawblade GameObject
            Debug.Log("Sawblade destroyed!");
        }
        else
        {
            Debug.LogWarning("Sawblade is already null or not assigned.");
        }
    }
}
