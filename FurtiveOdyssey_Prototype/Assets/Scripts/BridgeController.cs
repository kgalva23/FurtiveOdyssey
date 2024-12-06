using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] float fallDelay = 1.0f; // Delay before the segment falls

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.isKinematic = true; // Temporarily disable gravity
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        //if Red button interacts with Ross, add 1 to score & destroy Coin
        if (collision.gameObject.name == "Ross") {
            Debug.Log("Jumped on Bridge!");
            StartCoroutine(EnableFallAfterDelay());
        }
    }

    IEnumerator EnableFallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);

        rb.isKinematic = false; // Enable gravity after the delay
        Debug.Log($"{gameObject.name} is now falling!");
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy the segment when it goes out of camera bounds
    }
}
