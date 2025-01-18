using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CamMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0,transform.position.y, _target.position.z + _offset.z), _speed * Time.deltaTime);
    }
}
