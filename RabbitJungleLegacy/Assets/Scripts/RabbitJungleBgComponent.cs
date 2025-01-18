using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RabbitJungleBgComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private Vector3 _onOffDirections;

    [SerializeField]
    private float _moveSpeed;

    public bool isMove;

    private void LateUpdate()
    {
        if (isMove)
            return;
        transform.position = Vector3.Lerp(transform.position, new Vector3((_target.position.x + _offset.x) * _onOffDirections.x, (_target.position.y + _offset.y) * _onOffDirections.y, (_target.position.z + _offset.z) * _onOffDirections.z), _moveSpeed * Time.deltaTime);
    }
}
