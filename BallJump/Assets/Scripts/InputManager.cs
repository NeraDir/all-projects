using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerClickHandler
{
    public delegate void ScreenTouch();
    public static event ScreenTouch ScreenTouchDetected;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ScreenTouchDetected != null)
        {
            ScreenTouchDetected();
        }
    }
}
