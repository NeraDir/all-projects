using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FOLLOWCOMPONENT : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private Vector3 _direction;

    [SerializeField] private float _speed;

    private void LateUpdate()
    {
        if (_target != null)
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(
                    _direction.x > 0 ? (_target.position.x + _offset.x) : transform.position.x,
                    _direction.y > 0 ? (_target.position.y + _offset.y) : transform.position.y,
                    _direction.z > 0 ? (_target.position.z + _offset.z) : transform.position.z),
                _speed * Time.deltaTime);
    }
}
