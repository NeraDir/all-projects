using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    private Joystick _floatingJoystick;

    public static Vector2 _moveDirection;
    void Awake()
    {
        _floatingJoystick = GetComponent<Joystick>();
        _moveDirection = Vector2.zero;
    }


    public void OnDrag(PointerEventData eventData)
    {

        _moveDirection = _floatingJoystick.Direction;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _moveDirection = Vector2.zero;
    }

    public void OnDisable()
    {
        _moveDirection = Vector2.zero;
    }
}
