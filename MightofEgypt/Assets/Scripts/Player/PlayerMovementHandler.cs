using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] private Joystick _moveJoystick;

    [SerializeField] private float _movementSpeed = 3f;

    [SerializeField] private PlayerAliveHandler _playerAliveHandler;

    private Rigidbody _rigidbody;

    private void Awake() {
        Initialize();
    }

    private void Initialize() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MovePlayer() {
        if (_playerAliveHandler.IsPlayerAlive) {
            Vector3 movement = transform.TransformDirection(new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical) * _movementSpeed);
            Vector3 resultMovement = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
            _rigidbody.velocity = resultMovement;
        }
    }

    public void FreezeMovement() => _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
}
