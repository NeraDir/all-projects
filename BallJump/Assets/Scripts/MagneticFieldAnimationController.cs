using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldAnimationController : MonoBehaviour
{
    private Animator myAnimator;
    private Transform childTransform;

    [SerializeField]
    private float rotateSpeed;

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
        childTransform = transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        if (childTransform != null)
        {
            childTransform.Rotate(0,0, rotateSpeed);
        }
        
    }

    public void PlayEnebleAnimation()
    {
        myAnimator.SetInteger("stateIndex", 0);
    }

    public void PlayIdleAnimation()
    {
        myAnimator.SetInteger("stateIndex", 1);
    }

    public void PlayDisableAnimation()
    {
        myAnimator.SetInteger("stateIndex", 2);
    }
}
