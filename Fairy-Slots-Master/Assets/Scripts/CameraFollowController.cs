using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollowController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offcet;
    private Vector3 _cameraPos;

    [SerializeField]
    private Transform _target;
    private Transform _myTransform;

    [SerializeField]
    private float _followSpeed;

    private void OnEnable()
    {
        _myTransform = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        _cameraPos = new Vector3(0, _target.position.y + _offcet.y, _target.position.z + _offcet.z);
        _myTransform.position = Vector3.Lerp(_myTransform.position, _cameraPos, _followSpeed);
    }
}
