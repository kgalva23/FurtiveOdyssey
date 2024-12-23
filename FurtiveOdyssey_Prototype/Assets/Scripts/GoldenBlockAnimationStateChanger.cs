using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBlockAnimationStateChanger : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] string currentState = "GoldenBlock";

    void Start(){
        ChangeAnimationState("GoldenBlock");
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
