using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonComponent : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Animator _mainPage;

    [SerializeField]
    private Animator _clsoePage;

    public static bool isClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        isClicked = true;
        StartCoroutine(ButtonFunction());
    }

    private IEnumerator ButtonFunction()
    {
        if(_clsoePage != null)
            _clsoePage?.SetBool("UI_PAGE_STATE", true);
        yield return new WaitForSeconds(0.5f);
        if (_clsoePage != null)
            _clsoePage?.gameObject.SetActive(false);
        if(_mainPage != null)
            _mainPage.gameObject.SetActive(true);
        isClicked = false;
    }
}
