using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CamGo : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float speed;

    private void LateUpdate()
    {
        if (_target != null)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, _target.position.z + offset.z),speed * Time.deltaTime);
    }
}
