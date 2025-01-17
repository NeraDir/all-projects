using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRotator : MonoBehaviour
{
    [SerializeField]
    private Joystick _floatingJoystick;
    [SerializeField]
    private float _rotationCoefficient;

    private RectTransform _planeTransform;

    private void Awake()
    {
        _planeTransform = GetComponent<RectTransform>();
    }
 
    void FixedUpdate()
    {
        if (_floatingJoystick.Direction != Vector2.zero)
        {
            _planeTransform.rotation = Quaternion.FromToRotation(transform.right, transform.right + new Vector3(Mathf.Abs(_floatingJoystick.Direction.x) , _floatingJoystick.Direction.y * _rotationCoefficient, 0f));
        }

    }
}
