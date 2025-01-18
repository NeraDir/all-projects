using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalJumpDead : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, transform.position.y, _target.position.z);
    }
}
