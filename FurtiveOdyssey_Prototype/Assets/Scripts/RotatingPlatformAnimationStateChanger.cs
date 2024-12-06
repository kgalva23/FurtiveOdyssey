using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformAnimationStateChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string currentState = "RotatingPlatform";
    [SerializeField] float speed = 0.7f;

    void Start()
    {
        ChangeAnimationState("RotatingPlatform", speed);
    }

    public void ChangeAnimationState(string newState, float newSpeed)
    {
        animator.speed = newSpeed; // Set the animator's playback speed
        if (currentState == newState)
        {
            return;
        }
        currentState = newState;
        animator.Play(currentState);

    }
}
