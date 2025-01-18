using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LostMenuButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject closeMenu;
    public GameObject openMenu;

    private bool opened;

    public void OnPointerClick(PointerEventData eventData)
    {
            if (opened)
                return;
            StartCoroutine(ClickState());
    }

    private IEnumerator ClickState() 
    {
        opened = true;
        closeMenu.GetComponent<Animator>().SetBool("MENUSTATE", true);
        yield return new WaitForSeconds(0.5f);
        closeMenu.SetActive(false);
        openMenu.SetActive(true);
        opened = false;
    }
}
