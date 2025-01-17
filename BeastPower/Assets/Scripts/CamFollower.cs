using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    [SerializeField]
    private Vector3 _enabledVectors;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3((_target.position + _offset).x * _enabledVectors.x, (_target.position + _offset).y * _enabledVectors.y, (_target.position + _offset).z * _enabledVectors.z), _speed * Time.deltaTime);
    }
}
