using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CamController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;

    private void LateUpdate() 
    {
        if (_target != null)
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);
    }
}
