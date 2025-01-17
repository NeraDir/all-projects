using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private Animator jAnimator;

    private void OnEnable()
    {
        jAnimator = GetComponent<Animator>();
    }

    public void SetIdleAnimationState()
    {
        jAnimator.SetInteger("animation_clip_index", 0);
    }
    public void SetTriggerAnimationState()
    {
        jAnimator.SetInteger("animation_clip_index", 1);
    }
    public void SetJumpAnimationState()
    {
        jAnimator.SetInteger("animation_clip_index", 2);
    }

    private Collision lastCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out HeadSweetie head) && lastCollision != collision)
        {
            lastCollision = collision;
            head.currentJelly = this;
            SetTriggerAnimationState();
        }
    }
    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HeadSweetie head) && lastCollision == collision)
        {
            head.currentJelly = null;
            //lastCollision = collision;
            SetIdleAnimationState();
            Debug.Log("exit");
        }
    }
    */
}
