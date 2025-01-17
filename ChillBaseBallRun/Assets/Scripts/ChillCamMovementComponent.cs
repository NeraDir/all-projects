using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ChillCamMovementComponent : MonoBehaviour
{
    [SerializeField]
    private Transform _chillTarget;

    [SerializeField]
    private Vector3 _chillOffset;

    [SerializeField]
    private float _chillSpeed;

    private void LateUpdate()
    {
        if (_chillTarget != null)
            transform.position = Vector3.Lerp(transform.position, _chillTarget.position + _chillOffset, _chillSpeed * Time.deltaTime);
    }
}
