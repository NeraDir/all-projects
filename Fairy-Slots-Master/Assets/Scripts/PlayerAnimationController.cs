using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _myAnimator;


    private void OnEnable()
    {
        _myAnimator = GetComponent<Animator>();
    }

    public void PlayLeftMoveAnimation()
    {
        _myAnimator.SetInteger("moveStateIndex", 2);
    }
    public void PlayRightMoveAnimation()
    {
        _myAnimator.SetInteger("moveStateIndex", 1);
    }
    public void PlayIdleAnimattion()
    {
        _myAnimator.SetInteger("moveStateIndex", 0);
    }

}
