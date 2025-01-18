using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonComponent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator _closePage;

    [SerializeField]
    private Animator _openPage;

    private float _waitTime = 0.5f;

    public static bool isClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        isClicked = true;
        StartCoroutine(OnButtonPressed());
    }

    private IEnumerator OnButtonPressed()
    {
        if (_closePage != null)
        {
            _closePage.SetBool("PAGE_INDEX", true);
        }
        yield return new WaitForSeconds(_waitTime);
        if (_closePage != null)
        {
            _closePage.gameObject.SetActive(false);
        }
        if (_openPage != null)
        {
            _openPage.gameObject.SetActive(true);
        }
        isClicked = false;
    }
}
