using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSwipeHandler :MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    public float deathLenghtValue;

    private Vector3 startSwipeVector;
    private Vector3 swipeDelatVector;

    public delegate void SwipeCompleteDelegate(Vector3 direction);
    public static event SwipeCompleteDelegate SwipeCompleteEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        startSwipeVector = eventData.position;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        swipeDelatVector = (Vector3)eventData.position - startSwipeVector;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        float swipeAngle = 0;
        Vector3 swipeDirection = Vector3.zero;

        if(swipeDelatVector.magnitude > deathLenghtValue)
        {
            swipeAngle = Vector3.Angle(swipeDelatVector, Vector3.right);


            if(swipeAngle <= 45f)
            {
                swipeDirection = new Vector3(1, 0, 0);
            }
            else if (swipeAngle > 45f && swipeAngle <= 135f)
            {
                if(swipeDelatVector.y > 0)
                {
                    swipeDirection = new Vector3(0, 1, 0);
                }
                else
                {
                    swipeDirection = new Vector3(0, -1, 0);
                }
                
            }
            else if(swipeAngle > 135f)
            {
                swipeDirection = new Vector3(-1, 0, 0);
            }

            if(SwipeCompleteEvent != null)
            {
                SwipeCompleteEvent(swipeDirection);
            }
        }

        startSwipeVector = swipeDelatVector = Vector3.zero;
    }

}
