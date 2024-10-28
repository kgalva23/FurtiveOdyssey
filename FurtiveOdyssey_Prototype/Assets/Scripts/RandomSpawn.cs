using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomSpawn : MonoBehaviour
{
    [SerializeField] List <GameObject> fallingObjects;

    [SerializeField] float timerInterval = 0.5f;

    float gameTimer;

    public int counter = 0;

    void FixedUpdate() {
        gameTimer += Time.deltaTime;    //set gameTimer to keep track of 

        //spawn objects every time interval is met, then reset timer
        if (gameTimer >= timerInterval && counter <= 20) {
            FallingObject();
            gameTimer = 0;
            counter++;
        }

    }

    public void FallingObject() {
        float randomPos = Random.Range (-7.0f, 125.0f);           //random starting position for objects
        //float speed = Random.Range(1.0f, 5.0f);                 //random speed for objects
        int randomNum = Random.Range(0, fallingObjects.Count);  //random object from fallingObjects list

        //Instatiate a falling object at a random postion
        GameObject objectFall = Instantiate(fallingObjects[randomNum], new Vector3(randomPos, -3.25f, 0f), Quaternion.identity);

        //Set the velocity of the falling object to be random from 1 to 5
        objectFall.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }

}
