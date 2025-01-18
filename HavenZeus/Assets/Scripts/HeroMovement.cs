using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField]
    private Joystick _fixedJoystick;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private float _movementSpeed;

    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_fixedJoystick.Direction != Vector2.zero)
        {
            _animator.SetBool("IsRun", true);
            _animator.SetFloat("RunSpeed", _fixedJoystick.Direction.magnitude * 1.5f);
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }

        _rigidbody.velocity = new Vector3(_fixedJoystick.Direction.x, 0f, _fixedJoystick.Direction.y) * _movementSpeed * Time.fixedDeltaTime;
        //_rigidbody.MovePosition(transform.position + new Vector3(_fixedJoystick.Direction.x, 0f, _fixedJoystick.Direction.y) * _movementSpeed * Time.deltaTime);


    }
}
