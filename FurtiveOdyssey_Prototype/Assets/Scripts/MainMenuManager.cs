using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //Load Gameplay scene so player can start playing game
    public void StartGame(){
        SceneManager.LoadScene("Gameplay");
    }

    //Quit the entire game 
    public void QuitGame(){
        Application.Quit(); //only works when we build the game

    }
}
