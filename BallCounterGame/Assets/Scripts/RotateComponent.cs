using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 Axis;
    [SerializeField]
    private float speed;
    private float speedLerp;

    private Transform myTransform;

    private void OnEnable()
    {
        myTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        speedLerp = Mathf.Lerp(speedLerp, speed, 0.3f);
        myTransform.Rotate(Axis * speedLerp);
    }

    public void ChangeSpeed(float value)
    {
        speed = value;
    }
}
