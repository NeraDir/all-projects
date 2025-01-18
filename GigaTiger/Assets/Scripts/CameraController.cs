using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    public Vector3 cameraOffcet;
    public Transform followTarget;
    public float followLerpValue;
    private Vector3 newCameraPosition;

    private void LateUpdate()
    {
        newCameraPosition = new Vector3(followTarget.position.x + cameraOffcet.x, transform.position.y, followTarget.position.z + cameraOffcet.z);
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, followLerpValue);
    }
}
