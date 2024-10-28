using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam;  // Main Camera
    Vector3 camStartPos;  // Starting position of the camera
    float distance;  // Distance between the camera start position and its current position

    GameObject[] backgrounds;  // Array of background game objects
    Material[] mat;  // Array of materials of the background objects
    float[] backSpeed;  // Parallax speeds for each background

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;  // Get the camera's transform
        camStartPos = cam.position;  // Store the initial camera position

        int backCount = transform.childCount;  // Number of backgrounds (children of the parent object)
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;  // Get each child background object
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;  // Get the material of each background
        }
        BackSpeedCalculate(backCount);  // Calculate the speed for each background based on distance from the camera
    }

    // Function to calculate parallax speed based on depth (distance from the camera)
    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            float backgroundDepth = backgrounds[i].transform.position.z - cam.position.z;
            if (backgroundDepth > farthestBack)
            {
                farthestBack = backgroundDepth;  // Find the farthest background to calculate relative speeds
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;  // Calculate parallax speed
        }
    }

    // Update is called once per frame after the camera has moved
    void LateUpdate()
    {
        // Calculate how far the camera has moved from its starting position
        distance = cam.position.x - camStartPos.x;

        // Loop through all background layers and update their texture offset for parallax effect
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;  // Calculate how much to move the background based on parallax speed
            Vector2 offset = new Vector2(distance * speed, 0);  // Compute the offset for horizontal movement
            mat[i].SetTextureOffset("_MainTex", offset);  // Apply the offset to the background texture
        }
    }
}
