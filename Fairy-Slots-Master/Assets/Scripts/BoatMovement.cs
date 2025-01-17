using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField]
    private float _distanceBetweenLines;

    private LineState _currentState;

    [SerializeField]
    private BoatLookPointController boatLookPointController;
    private Transform _lookPointTargetTransform;

    private Transform _myTransform;

    [SerializeField]
    private float _moveForwardSpeed;
    [SerializeField]
    private float _switchLineSpeed;

    private float _lookPointTagetXpos;


    [SerializeField]
    private PlayerAnimationController playerAnimationController;

    private bool isInit;


    private void OnEnable()
    {
        UI_GameController.TapLeftButton += MoveLeft;
        UI_GameController.TapRightButton += MoveRight;

    }
    private void OnDisable()
    {
        UI_GameController.TapLeftButton -= MoveLeft;
        UI_GameController.TapRightButton -= MoveRight;

    }


    public void Init(float moveForwardSpeed)
    {
        _myTransform = GetComponent<Transform>();
        _lookPointTargetTransform = boatLookPointController.transform;
        _currentState = LineState.midle;
        _lookPointTagetXpos = 0;
        _moveForwardSpeed = moveForwardSpeed;
        boatLookPointController.Init(_myTransform, _distanceBetweenLines, _switchLineSpeed);

        isInit = true;
    }


    private void FixedUpdate()
    {
        if (isInit)
        {
            _myTransform.LookAt(_lookPointTargetTransform);
            _myTransform.position += _myTransform.forward * _moveForwardSpeed;
        }
    }

    public void MoveLeft()
    {
        Debug.Log("Left");

        if (_currentState != LineState.left)
        {

            if (_currentState == LineState.right)
            {
                _currentState = LineState.midle;
            }
            else
            {
                _currentState = LineState.left;
            }

            playerAnimationController.PlayLeftMoveAnimation();

            _lookPointTagetXpos -= _distanceBetweenLines;
        }

        boatLookPointController.SwitchLine(_currentState, _lookPointTagetXpos);
    }
    public void MoveRight()
    {
        Debug.Log("Right");

        if (_currentState != LineState.right)
        {
            if (_currentState == LineState.left)
            {
                _currentState = LineState.midle;
            }
            else
            {
                _currentState = LineState.right;
            }

            playerAnimationController.PlayRightMoveAnimation();

            _lookPointTagetXpos += _distanceBetweenLines;
        }
    
        boatLookPointController.SwitchLine(_currentState, _lookPointTagetXpos);
    }

}

public enum LineState
{
    left,
    midle,
    right
}
