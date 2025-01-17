using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCamFollowerComponent : MonoBehaviour
{
    public Transform thunderPlaneTarget;

    public Vector3 thunderOffset;

    public float thunderMoveSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, thunderPlaneTarget.position + thunderOffset, thunderMoveSpeed * Time.deltaTime);
    }
}
