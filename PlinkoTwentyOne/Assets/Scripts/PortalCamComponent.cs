using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PortalCamComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector3 _offset;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);
    }
}
