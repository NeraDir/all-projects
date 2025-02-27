using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BettersCustomButton : MonoBehaviour
{
    [SerializeField] private Animator _closePageAnimator;
    [SerializeField] private Animator _openPageAnimator;
    [SerializeField] private UnityEvent _onClickAction = new UnityEvent();

    public static bool isClicked;

    private Vector3 _scale;
    private AudioClip _clip;

    private void Start()
    {
        _clip = Resources.Load("Sound/press") as AudioClip;
        _scale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (isClicked)
            return;
        isClicked = true;
        BettersMusicComponent.instance.playSound?.Invoke(_clip);
        transform.DOScale(_scale / 1.3f, 0.1f).OnComplete(() => transform.DOScale(_scale * 1.2f, 0.1f).OnComplete(() => transform.DOScale(_scale, 0.1f).OnComplete(() => StartCoroutine(OnClick()))));
    }

    private IEnumerator OnClick()
    {
        if (_closePageAnimator != null) _closePageAnimator.SetBool("BettersAnimationKey", true);
        yield return new WaitForSeconds(0.5f);
        if (_closePageAnimator != null) _closePageAnimator.gameObject.SetActive(false);
        if (_openPageAnimator != null) _openPageAnimator.gameObject.SetActive(true);
        isClicked = false;
        _onClickAction?.Invoke();
    }
}
