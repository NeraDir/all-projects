using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GameCamComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _speed;

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.position.z + _offset.z);
    }
}
