using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMoveComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 _directionsActive;

    [SerializeField]
    private Transform _target;

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x * _directionsActive.x, _target.position.y * _directionsActive.y, _target.position.z * _directionsActive.z);
    }
}
