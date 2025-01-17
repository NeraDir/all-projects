using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffaloSwipes : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    private float _deadZone = 10;

    private Vector2 swipeDelta, startTouch;

    [SerializeField]
    private Transform _buffaloTransform;

    private bool _buffaloCanMove;

    private void Start()
    {
        _buffaloCanMove = true;
    }

    private void LateUpdate()
    {
        if (BuffaloRunOwlComponent.isStop)
            return;
        if (swipeDelta.magnitude > _deadZone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    if (!_buffaloCanMove)
                        return;
                    _buffaloCanMove = false;
                    _buffaloTransform.DOMoveX(-0.739f, 0.1f).OnComplete(() => _buffaloCanMove = true);
                }
                else
                {
                    if (!_buffaloCanMove)
                        return;
                    _buffaloCanMove = false;
                    _buffaloTransform.DOMoveX(0.739f, 0.1f).OnComplete(() => _buffaloCanMove = true);
                }
            }
            else
            {
                if (y > 0)
                {
                    if (!_buffaloTransform.GetComponent<BuffaloRunOwlComponent>().buffaloOnTheGround)
                        return;
                    _buffaloTransform.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.5f, ForceMode.Impulse);
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
