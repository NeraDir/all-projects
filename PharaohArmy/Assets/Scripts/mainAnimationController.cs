using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAnimationController : MonoBehaviour
{
    public AnimationSetter playerAnimator;

    public AnimationSetter enemieAnimator;

    public void SetPlayerAnimationActive() 
    {
        playerAnimator.SetState(1);
    }

    public void SetEnemieAnimationActive()
    {
        enemieAnimator.SetState(1);
    }

    public void SetPlayerAnimationIdle()
    {
        playerAnimator.SetState(0);
    }

    public void SetEnemieAnimationIdle()
    {
        enemieAnimator.SetState(0);
    }

    public void SetMeIdle() 
    {
        GameManager.cqanClick = false;
        GetComponent<Animator>().SetInteger("mainAnimator", 0);
    }
}
