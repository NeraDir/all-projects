using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator myAnimator;

    private void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
    }


    public void PlayWalkAnimation()
    {
        myAnimator.SetInteger("index", 0);
    }
    public void PlayAttackAnimation()
    {
        myAnimator.SetInteger("index", 1);
    }
    public void PlayDeadAnimation()
    {
        myAnimator.SetInteger("index", 2);
    }

}
