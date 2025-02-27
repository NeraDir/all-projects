using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChicoCustomButton : MonoBehaviour, IPointerClickHandler
{
    public Animator _closePage;
    public Animator _openPage;

    public static bool isClicked;

    public UnityEvent evente;

    public AudioClip clip;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        isClicked = true;
        transform.DOScale(0.9f, 0.1f).OnComplete(() => transform.DOScale(1.1f, 0.1f).OnComplete(() => transform.DOScale(1, 0.1f).OnComplete(() => StartCoroutine(Click()))));
    }

    private IEnumerator Click()
    {
        if (_closePage != null)
            _closePage.SetBool("ChicoUIIndex", true);
        SettingsManager.instance.onPlaySound?.Invoke(clip);
        yield return new WaitForSeconds(0.5f);
        if(_closePage != null)
            _closePage.gameObject.SetActive(false);
        if(_openPage != null)
            _openPage.gameObject.SetActive(true);
        isClicked = false;
        evente?.Invoke();
    }
}
