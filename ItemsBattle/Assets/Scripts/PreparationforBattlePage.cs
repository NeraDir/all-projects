using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreparationforBattlePage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject rulesPage;

    private Animator currentAnimator;
    private float waitTimeToChangeAnimation;

    public static bool canClickToScreen;

    public delegate void TapToScreenDelegate();
    public static event TapToScreenDelegate TapToScreenEvent;



    private void Awake()
    {
        currentAnimator = GetComponent<Animator>();

        waitTimeToChangeAnimation = 0;
        AnimationClip[] animationClips = currentAnimator.runtimeAnimatorController.animationClips;

        foreach (var clip in animationClips)
        {
            if (clip.name == "Open")
            {
                waitTimeToChangeAnimation = clip.length;
            }
        }

    }

    private void OnEnable()
    {
        Invoke(nameof(PlayLoopAnimation), waitTimeToChangeAnimation);
    }



    public void PlayOpenAnimation()
    {
        currentAnimator.SetInteger("ClipIndex", 0);
    }
    public void PlayLoopAnimation()
    {
        currentAnimator.SetInteger("ClipIndex", 1);
    }
    public void PlayCloseAnimation()
    {
        currentAnimator.SetInteger("ClipIndex", 2);

        float closeAnimationLenght = 0;

        AnimationClip[] animationClips = currentAnimator.runtimeAnimatorController.animationClips;

        foreach (var clip in animationClips)
        {
            if (clip.name == "Close")
            {
                closeAnimationLenght = clip.length;
            }
        }

        Invoke(nameof(DisabledPage), closeAnimationLenght);

    }


    public void DisabledPage()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canClickToScreen)
        {
            canClickToScreen = false;
            if (TapToScreenEvent != null)
                TapToScreenEvent();
        }
    }

    public void ShowRules()
    {
        Time.timeScale = 0;
        canClickToScreen = false;
        rulesPage.SetActive(true);
    }
    public void CloseRules()
    {
        Time.timeScale = 1;
        canClickToScreen = true;
        rulesPage.SetActive(false);
    }
}
