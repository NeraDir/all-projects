using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraMoveJoystick : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;

    private Quaternion lastRotation;

    

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position + Offset, 500f * Time.deltaTime);
        transform.rotation = Player.parent.parent.parent.parent.parent.parent.rotation;
        /*if (GameManager.Instance.CameraJoystick.Vertical != 0 || GameManager.Instance.CameraJoystick.Horizontal != 0)
        {
            float _targetRotation = Mathf.Atan2(GameManager.Instance.CameraJoystick.Horizontal, GameManager.Instance.CameraJoystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = GetRotation(transform, new Quaternion(0, _targetRotation, 0, 0));
            lastRotation = transform.rotation;
        }
        else
            transform.rotation = lastRotation;*/
    }

    private Quaternion GetRotation(Transform transform, Quaternion rotation)
    {
        float RotationSmoothTime = 0.1f;
        float _rotationVelocity = 0;
        return Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation.y, ref _rotationVelocity,
                RotationSmoothTime), 0);
    }
}
