using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationHandler : MonoBehaviour
{
    [SerializeField] private Joystick _rotationJoystick;

    [SerializeField] private float _rotationSpeed = 5f;

    [SerializeField] private Transform _mummy;

    [SerializeField] private Vector3 _lookAtMummyOffset;

    private float _xRotation = 0;

    private float _maximumXRotation = 90f;

    [SerializeField] private PlayerAliveHandler _playerAliveHandler;

    private void Update() => RotateCamera();

    private void RotateCamera() {
        if (_playerAliveHandler.IsPlayerAlive) {
            _xRotation += (-_rotationJoystick.Vertical * _rotationSpeed) * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, -_maximumXRotation, _maximumXRotation);
            transform.rotation = Quaternion.Euler(_xRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    public void LookAtMummy() => transform.LookAt(_mummy.position + _lookAtMummyOffset);
}
