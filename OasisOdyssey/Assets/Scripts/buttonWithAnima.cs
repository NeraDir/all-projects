using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonWithAnima : MonoBehaviour, IPointerClickHandler
{
    public Animator parentAnimator;

    public GameObject needOpenAniamtor;

    private bool isAniamtionPlayeing;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isAniamtionPlayeing)
        {
            return;
        }

        StartCoroutine(AnimationPlayingAfterOpenOtherAnimator());
    }

    private IEnumerator AnimationPlayingAfterOpenOtherAnimator() 
    {
        isAniamtionPlayeing = true;
        parentAnimator.SetBool("anim_state", true);
        yield return new WaitForSeconds(0.5f);
        parentAnimator.gameObject.SetActive(false);
        needOpenAniamtor.SetActive(true);
        isAniamtionPlayeing = false;
    }
}
