using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetectOnMobile : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    private const float DEADZONE = 0.0f;

    public static SwipeDetectOnMobile instance { get; set; }

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;

    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // Rest all bool
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        // DeadZone
        if (swipeDelta.magnitude > DEADZONE)
        {
            // Cool swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or Right
                if (x < 0)
                {
                    // Left
                    swipeLeft = true;
                }
                else
                {
                    // Right
                    swipeRight = true;
                }
            }
            else
            {
                // Up or Down
                if (y < 0)
                {
                    // Down
                    swipeDown = true;
                }
                else
                {
                    // Up
                    swipeUp = true;
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
