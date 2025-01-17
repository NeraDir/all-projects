using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonForCar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CarMoving _carMoving;
    [SerializeField] private float _dir = -1;
    [SerializeField] private bool isVertical = true;
    public static int x = 0;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isVertical)
        {
            _carMoving.SetV(_dir);
        }
        else
        {
            _carMoving.SetH(_dir);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isVertical)
        {
            _carMoving.SetV(0);
        }
        else
        {
            _carMoving.SetH(0);
        }
    }
}
