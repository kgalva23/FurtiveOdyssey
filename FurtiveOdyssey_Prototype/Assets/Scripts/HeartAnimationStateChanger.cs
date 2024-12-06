using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimationStateChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Heart";

    void Start(){
        ChangeAnimationState("Heart");
    }

    public void ChangeAnimationState(string newState, float speed = 1)
    {
        animator.speed = speed;
        if (currentState == newState){
            return;
        }
        currentState = newState;
        animator.Play(currentState);

    }
}
