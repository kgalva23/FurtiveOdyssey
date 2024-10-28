using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelRecapScript : MonoBehaviour
{
    public TextMeshProUGUI LevelRecap;             // Reference to LevelRecap text UI
    public EnemySpriteController enemyController;  // Reference to EnemySpriteController script

    void Start()
    {
        if (enemyController == null)
        {
            enemyController = FindObjectOfType<EnemySpriteController>();
            if (enemyController == null)
            {
                Debug.LogError("EnemySpriteController not found in the scene!");
            }
        }
    }

    public void DisplayLevelRecap()
    {
        // Update the Coin Score text
        int coinScore = ScoreCounter.Instance.totalCoinScore;

        // Update the Enemies Killed text
        int numEnemiesKilled = enemyController.getNumOfEnemiesKilled();

        // Update the Completion Time text
        float completionTime = LevelTimer.Instance.GetElapsedTime();
        LevelRecap.text = "Score: " + coinScore + "\nEnemies Killed: " + numEnemiesKilled + "\nCompletion Time: " + completionTime.ToString("F2") + "s";
    }

}
