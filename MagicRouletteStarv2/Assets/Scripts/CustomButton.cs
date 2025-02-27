using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _closePage;
    [SerializeField] private GameObject _openPage;

    [SerializeField] private UnityEvent _someAction;

    [SerializeField] private GameObject _panel;

    public static bool isPressed;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPressed)
            return;
        if (_panel != null && _panel.activeInHierarchy)
            return;
        isPressed = true;
        transform.DOScale(Vector3.one / 2, 0.1f).OnComplete(() => transform.DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(() => transform.DOScale(Vector3.one, 0.1f).OnComplete(() => StartCoroutine(DoSomthing()))));
    }

    private IEnumerator DoSomthing()
    {
        if (_closePage != null)
            _closePage.GetComponent<Animator>().SetInteger("CrystallsPageIndex", 1);
        yield return new WaitForSeconds(0.5f);
        if (_closePage != null)
            _closePage.SetActive(false);
        if(_openPage != null)
            _openPage.SetActive(true);
        isPressed = false;
        _someAction?.Invoke();
    }
}
