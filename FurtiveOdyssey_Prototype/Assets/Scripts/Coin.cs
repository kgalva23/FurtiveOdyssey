using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public int totalCoinScore = 0;  //initialize coin score to 0

    void OnTriggerEnter2D(Collider2D other) {
        //if Coin interacts with Ross, add 1 to score & destroy Coin
        if (other.gameObject.name == "Ross") {
            ScoreCounter.Instance.AddToScore(1);
            Debug.Log("Coin collected");
            Destroy(gameObject);
        }
    }

}
