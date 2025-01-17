using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class cammove : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _smooth;

    [SerializeField]
    private Vector3 _offset;

    private void LateUpdate()
    {
        if (_target != null)
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, _target.position.y + _offset.y, _target.position.z + _offset.z), _smooth * Time.deltaTime);
    }
}
