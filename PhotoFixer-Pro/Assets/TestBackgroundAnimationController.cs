using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBackgroundAnimationController : MonoBehaviour
{
    private Animator animator;

    private float animatorDefaultSpeed;
    private float animatorCurrentSpeed;
    private float animatorLerpSpeed;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animatorDefaultSpeed = animator.speed;
        animatorLerpSpeed = 0f;
       
        StartCoroutine(changeStartSpeed());
    }

    private void FixedUpdate()
    {
        animatorLerpSpeed = Mathf.Lerp(animatorLerpSpeed, animatorCurrentSpeed, 0.1f);
        animator.speed = animatorLerpSpeed;
    }

    private IEnumerator changeStartSpeed()
    {
        while(true)
        {
            animatorCurrentSpeed = animatorDefaultSpeed * 0.5f;
            yield return new WaitForSeconds(5f);
            animatorCurrentSpeed = animatorDefaultSpeed * 0.3f;
            yield return new WaitForSeconds(5f);
          
        }
    }

}
