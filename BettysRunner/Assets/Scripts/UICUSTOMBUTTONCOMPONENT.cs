using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UICUSTOMBUTTONCOMPONENT : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator _animatorClose;
    
    [SerializeField]
    private Animator _animatorOpen;

    [SerializeField]
    private Animator _betweenAnimtor;

    [SerializeField]
    private UnityEvent _action;

    public static bool buttonClicked;

    private AudioClip _clip;

    private void Awake()
    {
        _clip = Resources.Load("Audio/click") as AudioClip;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonClicked) return;
        buttonClicked = true;
        if (_clip != null) SETTINGSMANAGER.playSound?.Invoke(_clip);
        transform.DOScale(Vector3.one * 1.3f, 0.1f).OnComplete(() => transform.DOScale(Vector3.one / 1.1f,0.1f).OnComplete(() => transform.DOScale(Vector3.one,0.1f).OnComplete(() =>
        {
            StartCoroutine(ClickMotion());
        })));
    }

    private IEnumerator ClickMotion()
    {
        if (_animatorClose != null) _animatorClose.SetBool("BettysPageState", true);
        yield return new WaitForSeconds(0.5f);

        if (_betweenAnimtor != null) _betweenAnimtor.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _action?.Invoke();

        
        yield return new WaitForSeconds(0.5f);
        if (_betweenAnimtor != null) _betweenAnimtor.gameObject.SetActive(false);
        if (_animatorOpen != null) _animatorOpen.gameObject.SetActive(true);
        buttonClicked = false;
        if (_animatorClose != null) _animatorClose.gameObject.SetActive(false);
    }
}
