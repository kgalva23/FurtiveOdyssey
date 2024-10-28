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
}
