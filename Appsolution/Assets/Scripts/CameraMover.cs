using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField]
    private Transform _cameraTarget;
    [SerializeField]
    private float _cameraOffsetTime;


    private Vector3 _cameraVelocity;
    private Vector3 _cameraOffset;

    private void Awake()
    {
        _cameraOffset = transform.position - _cameraTarget.position;
    }


    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _cameraTarget.position + _cameraOffset, ref _cameraVelocity, _cameraOffsetTime);
    }
}
