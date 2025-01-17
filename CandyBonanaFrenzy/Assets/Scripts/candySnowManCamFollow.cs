using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class candySnowManCamFollow : MonoBehaviour
{
    public Transform SnowManTransform;

    public Vector3 FollowingOffset;

    public float FollowingSpeed;

    private void LateUpdate()
    {
        if (SnowManTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, SnowManTransform.position + FollowingOffset, FollowingSpeed * Time.deltaTime);
            transform.LookAt(SnowManTransform.position);
        }
    }
}
