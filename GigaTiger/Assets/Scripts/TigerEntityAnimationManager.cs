using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerEntityAnimationManager : MonoBehaviour
{
    private Animator tigerAnimator;


    private void OnEnable()
    {
        tigerAnimator = GetComponent<Animator>();
    }


    public void ChangeToKachAnimation()
    {
        tigerAnimator.SetInteger("p_id", 0);
    }
    public void ChangeToUpAnimation()
    {
        tigerAnimator.SetInteger("p_id", 1);
    }
    public void ChangeToRunAnimation()
    {
        tigerAnimator.SetInteger("p_id", 2);
    }
    public void ChangeToWalkAnimation()
    {
        tigerAnimator.SetInteger("p_id", 3);
    }
    public void ChangeToFallAnimatiob()
    {
        tigerAnimator.SetInteger("p_id", 4);
    }
    public void ChangeSlidingAnimation()
    {
        tigerAnimator.SetInteger("p_id", 5);
    }
    public void ChacngeToBananFallAnimation()
    {
        tigerAnimator.SetInteger("p_id", 6);
    }
    public void ChacngeToJumpAnimation()
    {
        tigerAnimator.SetInteger("p_id", 7);
    }

}
