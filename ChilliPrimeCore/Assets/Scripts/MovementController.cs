using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _particleSystem;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Quaternion _lastRotation;

    private float _rotationVelocity = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null) return;
        if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
        {
            Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

            Rotate(moveDirection);

            _rigidbody.velocity = new Vector3(moveDirection.x * _speed, _rigidbody.velocity.y, moveDirection.z * _speed);
            _animator.SetBool("Character", true);
            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }
        else
        {
            _animator.SetBool("Character",false);
            transform.rotation = _lastRotation;
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
            }
        }
    }

    private void Rotate(Vector3 input)
    {
        float _targetRotation = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, 0.04f), 0);
        _lastRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ChilliComponent chilli))
        {
            chilli.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => 
            {
                GameController.currentCount += 1;
                Destroy(chilli.gameObject);
            });
        }
    }
}
