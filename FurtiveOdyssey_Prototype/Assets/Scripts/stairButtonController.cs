using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairButtonController : MonoBehaviour
{
    [SerializeField] Sprite buttonUnpressed;  // Sprite when button is unpressed
    [SerializeField] Sprite buttonPressed;    // Sprite when button is pressed

    public SpriteRenderer buttonSprite;     // Button's Sprite Renderer
    public GameObject stairPrefab;          // Prefab for the stair steps
    public Transform startPoint;            // Starting point for the stairs
    public float stairDelay = 0.5f;         // Delay between spawning each stair
    public float stairDuration = 4f;        // Duration before stairs disappear

    private bool isButtonPressed = false;    // Track if button is pressed or not
    private int objectsOnButton = 0;         // Counter to track how many objects are on the button
    private List<GameObject> spawnedStairs = new List<GameObject>(); // Track spawned stairs

    void Awake() 
    {
        buttonSprite = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box")) && !isButtonPressed)
        {
            objectsOnButton++;
            buttonSprite.sprite = buttonPressed;
            isButtonPressed = true;
            StartCoroutine(SpawnStairs());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            objectsOnButton--;
            if (objectsOnButton <= 0)
            {
                buttonSprite.sprite = buttonUnpressed;
                isButtonPressed = false;
                objectsOnButton = 0;
            }
        }
    }

    IEnumerator SpawnStairs()
    {
        Vector3 stairPosition = startPoint.position; // Initial position for the first stair
        for (int i = 0; i < 8; i++)  // Adjust the count for the number of stairs you want
        {
            GameObject stair = Instantiate(stairPrefab, stairPosition, Quaternion.identity);
            spawnedStairs.Add(stair);  // Add stair to the list
            stairPosition += new Vector3(1, 1, 0);  // Diagonal offset (adjust as needed)
            yield return new WaitForSeconds(stairDelay);
        }

        yield return new WaitForSeconds(stairDuration);

        // Destroy stairs after the duration
        foreach (GameObject stair in spawnedStairs)
        {
            Destroy(stair);
        }
        spawnedStairs.Clear();
    }
}
