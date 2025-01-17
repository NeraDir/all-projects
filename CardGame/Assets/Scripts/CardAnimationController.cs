using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimationController : MonoBehaviour
{

    private Animator myAnimator;


    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
    }

    //StateNumber

    public void PlayEmptyAnimation()
    {//0
        myAnimator.SetInteger("StateNumber", 0);
    }
    public void PlayRotateToBackSideAnimation()
    {//1
        myAnimator.SetInteger("StateNumber", 1);
    }
    public void PlayRotateToFrontSideAnimation()
    {//2
        myAnimator.SetInteger("StateNumber", 2);
    }
    public void PlayTargetAnimation()
    {//4
        //myAnimator.SetInteger("StateNumber", 4);
    }
    public void PlayDestroyAnimation()
    {//3
        myAnimator.SetInteger("StateNumber", 3);
    }
}