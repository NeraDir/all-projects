using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public Vector3 offest;

    public float speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offest, speed * Time.deltaTime);
    }
}
