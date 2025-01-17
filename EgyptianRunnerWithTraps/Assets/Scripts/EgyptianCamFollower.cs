using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EgyptianCamFollower : MonoBehaviour
{
    [SerializeField]
    private Transform moveTarget;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float moveSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, moveTarget.position + offset, moveSpeed * Time.deltaTime);
    }
}
