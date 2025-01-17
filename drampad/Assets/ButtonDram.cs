using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDram : MonoBehaviour,/* IPointerUpHandler,*/ IPointerDownHandler
{
    [SerializeField] private int _num;
    [SerializeField] private Image _imageLight;
    [SerializeField] private MainMenager _mainMenager;
    private void Start()
    {
        _imageLight.enabled = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _mainMenager.Click(_num);
        StartCoroutine(Sh());
    }
    public void Show()
    {

        StartCoroutine(Sh());

    }
    private IEnumerator Sh()
    {
        _imageLight.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _imageLight.enabled = false;
    }

    /*public void OnPointerUp(PointerEventData eventData)
    {

    }*/
}
