using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButtonUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _closeAnimator;
    [SerializeField] private Animator _openAnimator;
    [SerializeField] private UnityEvent _clickAction;
    [SerializeField] private AudioClip _clickSound;
     
    public static bool isClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        isClicked = true;
        SettingsManager.playSound?.Invoke(_clickSound);
        transform.DOScale(Vector3.one/1.5f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>
            {
                StartCoroutine(DoMotion());
            });
        });
    }

    private IEnumerator DoMotion()
    {
        if(_closeAnimator != null)
            _closeAnimator.SetBool("TigerUIState",true);
        yield return new WaitForSeconds(0.5f);
        if(_closeAnimator != null)
            _closeAnimator.gameObject.SetActive(false);
        if (_openAnimator != null)
            _openAnimator.gameObject.SetActive(true);
        _clickAction?.Invoke();
        isClicked = false;
    }
}
