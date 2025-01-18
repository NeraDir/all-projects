using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationHandler : MonoBehaviour
{
    [SerializeField] private Joystick _rotationJoystick;

    [SerializeField] private float _rotationSpeed = 5f;

    [SerializeField] private Transform _mummy;

    [SerializeField] private PlayerAliveHandler _playerAliveHandler;

    private void FixedUpdate() => RotatePlayerHorizontally();

    private void RotatePlayerHorizontally() {
        if (_playerAliveHandler.IsPlayerAlive) {
            transform.Rotate(transform.up * (_rotationJoystick.Horizontal * _rotationSpeed), Space.Self);
        }
    }

    public void LookAtMummy() => transform.LookAt(_mummy.position);
}
