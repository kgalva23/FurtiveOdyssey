using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;
    public int totalCoinScore = 0;

    void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  //Persist this across scenes if needed
            SceneManager.sceneLoaded += OnSceneLoaded;  //Subscribe to scene load event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Method to reset Coin score when a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetCoinScore();  //Reset score when switching scenes
    }

    //Method to add to the Coin score
    public void AddToScore(int scoreToAdd)
    {
        totalCoinScore += scoreToAdd;
        Debug.Log("Total coin score: " + totalCoinScore);
    }

    //Method to reset the Coin score
    public void ResetCoinScore()
    {
        totalCoinScore = 0;
        Debug.Log("Score reset. Total coin score: " + totalCoinScore);
    }

    void OnDestroy() 
    {
        //Unsubscribe from the sceneLoaded event when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
}
