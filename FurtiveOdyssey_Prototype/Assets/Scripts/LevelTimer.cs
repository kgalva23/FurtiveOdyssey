using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;
    public float elapsedTime = 0f;
    public bool isTimerRunning = false;

    void Awake()
    {
        // Ensure only one instance of the timer exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: keeps the timer across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartTimer();  // Start the timer at the beginning of the level
    }

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;  // Increment the timer by the time passed each frame
        }
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }
    
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = false;  // Stop the timer to avoid it running on restart
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
