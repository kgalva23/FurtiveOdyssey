using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesPopUpController : MonoBehaviour
{
    // Settings for spike behavior
    public float speed = 2.0f;
    public float minWaitTime = 1.0f;
    public float maxWaitTime = 3.0f;
    public float popUpHeight = 1.0f;

    public List<Transform> spikes = new List<Transform>();

    void Start()
    {
        // Find all child spikes and store them
        foreach (Transform child in transform)
        {
            spikes.Add(child);
        }

        // Start pop-up behavior for each spike independently
        foreach (Transform spike in spikes)
        {
            StartCoroutine(RandomPopUpDown(spike));
        }
    }

    IEnumerator RandomPopUpDown(Transform spike)
    {
        // Store the initial and target positions
        Vector3 downPosition = spike.position;
        Vector3 upPosition = new Vector3(spike.position.x, spike.position.y + popUpHeight, spike.position.z);
        
        bool isUp = false;

        while (true)
        {
            // Wait for a random time before changing state
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);

            // Toggle between up and down positions
            isUp = !isUp;
            Vector3 targetPosition = isUp ? upPosition : downPosition;

            // Move the spike to the target position
            while (Vector3.Distance(spike.position, targetPosition) > 0.01f)
            {
                spike.position = Vector3.MoveTowards(spike.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
