using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    // This function is called when another collider enters the trigger collider attached to the sword
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided is tagged as "Enemy"
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Call the enemy's death or damage function
            EnemySpriteController enemy = other.GetComponent<EnemySpriteController>();
            if (enemy != null)
            {
                Destroy(gameObject, 1f);
            }
        }
    }

}
