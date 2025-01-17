using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class uiClicker : MonoBehaviour, IPointerClickHandler
{
    public GameObject openPage;

    public GameObject closePage;

    public bool isClickedToOpenPage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClickedToOpenPage)
            return;
        isClickedToOpenPage = true;
        StartCoroutine(StartASnimatipon());
    }

    private IEnumerator StartASnimatipon() 
    {
        closePage.GetComponent<Animator>().SetBool("isAnimatin", true);
        yield return new WaitForSeconds(0.5f);
        openPage.SetActive(true);
        closePage.SetActive(false);
        isClickedToOpenPage = false;
    }
}
