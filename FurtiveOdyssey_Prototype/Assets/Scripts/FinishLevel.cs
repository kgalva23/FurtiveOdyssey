using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    /*
    public void onTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player")) 
        {
            Debug.Log("Golden Block Triggered by Player");
            UnlockNewLevel();
        }
    }

    public void UnlockNewLevel() 
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex")) 
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            int newUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1) + 1;
            PlayerPrefs.SetInt("UnlockedLevel", newUnlockedLevel);
            PlayerPrefs.Save();
            Debug.Log("UnlockedLevel updated to: " + newUnlockedLevel);
        }
    }

    */

}