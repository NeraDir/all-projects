using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CandysCustomUIButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject openPanel;
    [SerializeField]
    private GameObject closePanel;

    private bool candyClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (candyClicked)
            return;
        candyClicked = true;
        StartCoroutine(StartOpening());
    }

    private IEnumerator StartOpening() 
    {
        closePanel.GetComponent<Animator>().SetInteger("CANDY_UI_ANIMATION_STATE", 1);
        yield return new WaitForSeconds(1);
        closePanel.gameObject.SetActive(false);
        openPanel.SetActive(true);
        candyClicked = false;
    }
}
