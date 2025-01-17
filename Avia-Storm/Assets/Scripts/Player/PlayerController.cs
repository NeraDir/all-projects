using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Movement PlayerMovement;
    public Joystick joystick;

    public Transform LeftTargetTransform;
    public Transform RightTargetTransform;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!PlayerMovement.InMovement)
        {
            PlayerMovement.InMovement = true;
            PlayerMovement.num = 8f;
            PlayerMovement.Move();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerMovement.InMovement && !PlayerMovement.InMovementPlane)
        {
            PlayerMovement.StopMove();
            PlayerMovement.InMovement = true;
            PlayerMovement.num = 3.5f;
            PlayerMovement.Move();
        }
    }
}
