using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : MonoBehaviour
{
    private Animator gAnimator;

    private void OnEnable()
    {
        gAnimator = GetComponent<Animator>();
    }

    public void SetIldeAnimationState()
    {
        gAnimator.SetInteger("animation_clip_index", 0);
    }
    public void SetOpenAnimationState()
    {
        gAnimator.SetInteger("animation_clip_index", 1);
    }
    public void SetOpenIdleAnimation()
    {
        gAnimator.SetInteger("animation_clip_index", 2);
    }
    public void SetCloseAninationState()
    {
        gAnimator.SetInteger("animation_clip_index", 3);
    }
}
