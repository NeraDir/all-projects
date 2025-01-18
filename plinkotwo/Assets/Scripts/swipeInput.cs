using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class swipeInput : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    private const float DEADZONE = 30.0f;

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;

    public static UnityEvent<float> jumpAndDown = new UnityEvent<float>();

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (swipeDelta.magnitude > DEADZONE)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                    jumpAndDown?.Invoke(-1);
                }
                else
                {
                    swipeUp = true;
                    jumpAndDown?.Invoke(1);
                }
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        startTouch = swipeDelta = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tap = true;
        startTouch = Input.mousePosition;
    }

    public void OnPointerMove(PointerEventData eventData)
    {

        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
    }
}
