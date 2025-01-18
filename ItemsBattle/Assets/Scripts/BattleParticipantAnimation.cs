using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleParticipantAnimation : MonoBehaviour
{
    private Animator currentAnimator;


    private AnimationClip showItemsClip;

    private void OnEnable()
    {
        currentAnimator = GetComponent<Animator>();

        AnimationClip[] animationClips = currentAnimator.runtimeAnimatorController.animationClips;

        foreach(var clip in animationClips)
        {
            if(clip.name == "ShowItems")
            {
                showItemsClip = clip;
            }
        }

    }


    public void PlaySpawnAnimation()
    {
        //0
        currentAnimator.SetInteger("ClipIndex", 0);
    }
    public void PlayShowItemsAnimation()
    {
        //1
        currentAnimator.SetInteger("ClipIndex", 1);
        Debug.Log(gameObject.name + "call Show Animation");

        float waintTime = showItemsClip.length;

        Invoke(nameof(PlayLoopAnimation), waintTime);
    }
    public void PlayLoopAnimation()
    {
        currentAnimator.SetInteger("ClipIndex", 2);
    }
    public void PlayDisableAnimation()
    {
        //3
        currentAnimator.SetInteger("ClipIndex", 3);
    }
    public void SetEmptyAnimation()
    {
        //4
        currentAnimator.SetInteger("ClipIndex", 4);
    }
    public void SetPositionToNextRoundAnimation()
    {
        currentAnimator.SetInteger("ClipIndex", 5);
    } 
}
