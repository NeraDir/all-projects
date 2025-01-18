using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _timeOffset;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _distanceOffset;

    private void Awake()
    {
        _distanceOffset = transform.position - _target.position;
    }
   
    void Update()
    {
       
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_target.position.x + _distanceOffset.x, transform.position.y, _target.position.z + _distanceOffset.z), ref _velocity, _timeOffset);
    }
}
