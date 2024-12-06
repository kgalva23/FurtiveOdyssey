using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InvisibleBlocks : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    [SerializeField] Tilemap tilemap; // Reference to your Tilemap
    [SerializeField] Vector3Int[] tilesToDestroy; // List of tiles to destroy (positions in grid coordinates)
    [SerializeField] GameObject sawblade;  // Reference to the sawblade GameObject

    [SerializeField] float segmentSpawnDelay = 0.2f;  // Delay between each segment spawn
    [SerializeField] int totalSegments = 5;
    [SerializeField] float transparencyDelay = 1f;  // Time before blocks turn invisible

    public bool buttonPressed = false;
    public bool spawningBridge = false;  // Flag to control scaling logic

    void OnCollisionEnter2D(Collision2D collision)
    {
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
            GameObject newSegment = Instantiate(bridge, segmentPosition, Quaternion.identity);

            // Start the coroutine to turn the block invisible after a delay
            StartCoroutine(TurnBlockInvisible(newSegment));

            // Move the segmentPosition for the next one
            segmentPosition += new Vector3(2.0f, 0, 0);  // Adjust this value based on the width of your segment

            yield return new WaitForSeconds(segmentSpawnDelay);  // Add delay between spawning segments
        }

        Debug.Log("Bridge fully extended!");
        spawningBridge = false;
    }

    // Coroutine to turn a block invisible after a delay
    IEnumerator TurnBlockInvisible(GameObject block)
    {
        yield return new WaitForSeconds(transparencyDelay); // Wait for the specified delay

        // Set the block's alpha transparency directly through the SpriteRenderer or Renderer
        SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0.02f; // Make fully transparent
            spriteRenderer.color = color;
            Debug.Log("Block turned invisible!");
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on the bridge object. Trying Renderer...");

            Renderer renderer = block.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;
                if (material != null)
                {
                    Color color = material.color;
                    color.a = 0f; // Make fully transparent
                    material.color = color;
                }
                else
                {
                    Debug.LogError("Renderer has no material assigned!");
                }
            }
        }

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
