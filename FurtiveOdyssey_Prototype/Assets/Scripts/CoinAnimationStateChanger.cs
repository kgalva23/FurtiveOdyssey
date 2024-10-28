using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimationStateChanger : MonoBehaviour
{
    // public enum AnimationState{Idle, Walk};
    // [SerializeField] AnimationState currentEnumState = AnimationState.Idle


    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Coin";

    void Start(){
        ChangeAnimationState("Coin");
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