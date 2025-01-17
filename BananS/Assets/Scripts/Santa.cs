using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    private Animator sAnimator;


    private void OnEnable()
    {
        sAnimator = GetComponent<Animator>();
    }

    public void SetIdleStateAnimation()
    {
        sAnimator.SetInteger("animation_clip_index", 0);
    }
    public void SetRotateStateAnimation()
    {
        sAnimator.SetInteger("animation_clip_index", 1);
    }
    public void SetDanceStateAnimation()
    {
        sAnimator.SetInteger("animation_clip_index", 2);
    }
}
