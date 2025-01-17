using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelloPage : MonoBehaviour, IPointerClickHandler
{
    public GameObject HomePage;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        HomePage.SetActive(true);
    }
}
