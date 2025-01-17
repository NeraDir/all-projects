using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuesAnimationController : MonoBehaviour
{
    private Animator myAnimator;

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
    }


    public void PlayIdleAnimation()
    {
        myAnimator.SetInteger("index", 0);
    }
    public void PlayAttackAnimation()
    {
        myAnimator.SetInteger("index", 1);
    }
    public void PlayJumpAttackAnimation()
    {
        myAnimator.SetInteger("index", 2);
    }

}
