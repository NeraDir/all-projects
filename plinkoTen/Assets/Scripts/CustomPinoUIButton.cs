using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CustomPinoUIButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Animator[] _closePage;

    [SerializeField]
    private Animator _openPage;

    public static bool isClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClicked)
            return;
        isClicked = true;
        StartCoroutine(OnClickedDO());
    }

    private IEnumerator OnClickedDO()
    {
        foreach (var item in _closePage)
            if (item != null)
                item.SetBool("PinoPageState", true);
        yield return new WaitForSeconds(0.5f);
        foreach (var item in _closePage)
            if (item != null)
                item.gameObject.SetActive(false);
        if (_openPage != null)
            _openPage.gameObject.SetActive(true);
        isClicked = false;
    }
}
