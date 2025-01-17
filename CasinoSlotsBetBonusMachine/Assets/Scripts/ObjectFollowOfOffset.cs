using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectFollowOfOffset : MonoBehaviour
{
    [SerializeField]
    private Transform targetFollow;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,targetFollow.position + offset, speed * Time.deltaTime);
    }
}
