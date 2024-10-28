using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsRespawn : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject playerFall = collision.gameObject;

        if (playerFall.name == "Ross")
        {
            LevelTimer.Instance.ResetTimer();
            EnemySpriteController.ResetEnemiesKilled();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
