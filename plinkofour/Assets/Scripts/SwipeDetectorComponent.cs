using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetectorComponent : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    private const float DEADZONE = 10.0f;

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;

    [SerializeField]
    private Transform _mainBallTransform;

    private bool _isMove;

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
                    if (_mainBallTransform.position.x - 2.19f < -2.19f)
                        return;
                    if (_isMove)
                        return;
                    _isMove = true;
                    _mainBallTransform.DOMoveX(-2.19f, 0.25f).OnComplete(()=> _isMove = false);
                    _mainBallTransform.DOMoveY(3.5f, 0.125f).OnComplete(() => _mainBallTransform.DOMoveY(0.9964247f, 0.125f));
                }
                else
                {
                    // Right
                    swipeRight = true;
                    if (_mainBallTransform.position.x + 2.19f > 2.19f)
                        return;
                    if (_isMove)
                        return;
                    _isMove = true;
                    _mainBallTransform.DOMoveX(2.19f, 0.25f).OnComplete(() => _isMove = false);
                    _mainBallTransform.DOMoveY(3.5f, 0.125f).OnComplete(() => _mainBallTransform.DOMoveY(0.9964247f, 0.125f));
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
