using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _target.position.z);
    }
}
