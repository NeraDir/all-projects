using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimationController : MonoBehaviour
{
    private Animator myAnimator;

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();

        if (myAnimator == null)
        {
            myAnimator = GetComponentInChildren<Animator>();
        }
    }


    public void PlayIdleAnimation()
    {
        myAnimator.SetInteger("AnimationIndex", 0);
    }
    public void PlayAttackAnimation()
    {
        myAnimator.SetInteger("AnimationIndex", 1);
    }
    public void PlayDeathAnimation()
    {
        myAnimator.SetInteger("AnimationIndex", 2);
    }
}
