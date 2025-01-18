using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyEntityAnimationsManager : MonoBehaviour
{
    private Animator monkeyAnimator;
    private Monkey parent;
    [SerializeField]
    private int attackAnimationRepeatCount;
    [SerializeField]
    private int currentRepeatCount;


    public void Init(Monkey parent, int attackCount)
    {
        currentRepeatCount = 0;
        this.parent = parent;
        attackAnimationRepeatCount = attackCount;
    }


    private void OnEnable()
    {
        monkeyAnimator = GetComponent<Animator>();
    }


    public void ChangeToIdleAnimation()
    {
        monkeyAnimator.SetInteger("p_id", 0);

    }
    public void ChangeToAttackAnimation()
    {
        monkeyAnimator.SetInteger("p_id", 1);
    }
    public void ChangeToDanceAnimation()
    {
        monkeyAnimator.SetInteger("p_id", 2);
    }

    public void RepeatAttackAnimation()
    {
        currentRepeatCount++;

        if (currentRepeatCount == attackAnimationRepeatCount)
        {
            ChangeToDanceAnimation();
        }
        else
        {
            //ChangeToAttackAnimation();
        }
    }

    public void CallAttackAnimationEvent()
    {
        parent.SpawnBanan();
    }
}
