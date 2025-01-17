using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAroundTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Transform m_Transform;

    [SerializeField]
    private float rotateSpeed;

    private void OnEnable()
    {
        m_Transform = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        m_Transform.RotateAround(target.position, Vector3.up, rotateSpeed);
    }
}
