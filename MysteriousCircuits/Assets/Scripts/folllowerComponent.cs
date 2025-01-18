using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class folllowerComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector3 _enableDirections;

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 targetVector = new Vector3((_target.position.x + _offset.x) * _enableDirections.x, (_target.position.y + _offset.y) * _enableDirections.y,(_target.position.z + _offset.z) * _enableDirections.z);
            transform.position = Vector3.Lerp(transform.position, targetVector, _speed * Time.deltaTime);
        }
    }
}
