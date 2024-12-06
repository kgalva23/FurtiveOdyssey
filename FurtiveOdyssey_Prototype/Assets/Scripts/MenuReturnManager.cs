using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReturnManager : MonoBehaviour
{
    //Return player back to Main Menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Replay Level for Player
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Load next Level for Player
    public void NextLevel()
    {
        // Get the current scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the next scene's build index
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the valid range of scenes in the build settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Next level does not exist!"); // Optional: Handle end-of-level logic here
        }
    }

    //Load next Level for Player
    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }    

}
