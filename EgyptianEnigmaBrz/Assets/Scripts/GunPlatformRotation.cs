using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlatformRotation : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    private Transform m_Transform;

    [SerializeField]
    private float rotationDefaultSpeed;
    private float rotationSpeed;
    private float rotationSpeedLerp;



    private void OnEnable()
    {
        m_Transform = GetComponent<Transform>();

        rotationSpeed = rotationDefaultSpeed;
        rotationSpeedLerp = 0;
    }

    private void Update()
    {
        rotationSpeedLerp = Mathf.MoveTowards(rotationSpeedLerp, rotationSpeed, 20f * Time.deltaTime);

        if (joystick.Horizontal !=  0)
        {
            rotationSpeed = rotationDefaultSpeed;
            m_Transform.Rotate(0, joystick.Horizontal * rotationSpeedLerp, 0);
        }
        else
        {
            rotationSpeed = 0;
            m_Transform.Rotate(0, 0, 0);
        }
       
    }



}
