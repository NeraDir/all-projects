using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetter : MonoBehaviour
{
    public Animator whatIsAnimator;

    public void SetState(int state) 
    {
        whatIsAnimator.SetInteger("animationStateIndex", state);
    }
}
