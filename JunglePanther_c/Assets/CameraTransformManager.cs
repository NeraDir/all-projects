using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransformManager : MonoBehaviour
{
    [SerializeField]
    private Transform _runnerTarget;

    [SerializeField]
    private Vector3 offcetVector;

    private Transform mTransform;

    private Vector3 followPos;

    private void Start()
    {
        mTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        followPos = new Vector3(mTransform.position.x, _runnerTarget.position.y + offcetVector.y, _runnerTarget.position.z + offcetVector.z);
        mTransform.position = Vector3.Lerp(transform.position, followPos, 0.3f);
    }
}
