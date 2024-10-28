using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeAnimationStateChanger : MonoBehaviour
{
    
    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Sawblade";

    void Start(){
        ChangeAnimationState("Sawblade");
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
