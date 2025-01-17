using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 CameraOffsets;
    public Transform Target;

    void LateUpdate()
    {
        if (Target != null)
            transform.position = Vector3.Lerp(transform.position, Target.position + CameraOffsets, 20f * Time.deltaTime);
    }
}
