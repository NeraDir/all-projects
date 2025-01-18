using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    public void PlayIdleAnimation()
    {
        playerAnimator.SetInteger("animationIndex", 0);
    }
    public void PlayWalkAnimation()
    {
        playerAnimator.SetInteger("animationIndex", 1);
    }
}
