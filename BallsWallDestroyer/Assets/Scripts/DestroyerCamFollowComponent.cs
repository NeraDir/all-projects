using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DestroyerCamFollowComponent : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float followSpeed;

    private void LateUpdate()
    {
        if (target != null)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
    }
}
