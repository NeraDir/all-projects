using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotions : MonoBehaviour
{
    public Transform target;
    private Transform myTransform;

    public float moveSpeed;


    private void OnEnable()
    {
        myTransform = GetComponent<Transform>();

        Ball.BallOnLastPoint += ChangeTarget;
        Ball.BallOnFirstPoint += ChangeTarget;
    }
    private void OnDisable()
    {
        Ball.BallOnLastPoint -= ChangeTarget;
        Ball.BallOnFirstPoint -= ChangeTarget;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, new Vector3(0, target.position.y, myTransform.position.z), moveSpeed);
        }
    }


    public void ChangeTarget(Transform targetPoint)
    {
        target = targetPoint;
    }


}
