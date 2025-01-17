using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HTPPage : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject menu;


    private void OnEnable()
    {

        menu.SetActive(false);
    }
    private void OnDisable()
    {
        menu.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }

    
}
