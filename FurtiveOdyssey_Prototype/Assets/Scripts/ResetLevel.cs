using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{

    // Reference to the WeaponSwitcher
    [SerializeField] BombController bombController;

    // Update is called once per frame
    void Update()
    {
        // Check the return value of isPlayerDestroyed() from BombController
        if (bombController.isPlayerDestroyed())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
