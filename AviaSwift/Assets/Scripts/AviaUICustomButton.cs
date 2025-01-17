using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AviaUICustomButton : MonoBehaviour, IPointerClickHandler
{
    public Animator panelAnimator;

    public GameObject panelOpener;

    public bool isExcitButton;

    private bool isButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isExcitButton) 
        {
            StartCoroutine(Exit());
        }
        else
        {
            if (isButton)
                return;
            isButton = true;
            StartCoroutine(Animations());
        }
    }

    private IEnumerator Exit() 
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    private IEnumerator Animations() 
    {
        panelAnimator.SetInteger("panrlAnima", 1);
        yield return new WaitForSeconds(0.5f);
        panelAnimator.gameObject.SetActive(false);
        panelOpener.SetActive(true);
        isButton = false;
    }
}
