using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Car _car;
    public void OnPointerDown(PointerEventData eventData)
    {
        _car.biasSpeed = -1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _car.biasSpeed = 0;
    }
}
