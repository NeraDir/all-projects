using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowByPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private Transform _myTransform;

    [SerializeField]
    private bool isWater;

    
    private float _offsetZ;



    private void OnEnable()
    {
        _myTransform = GetComponent<Transform>();

        _offsetZ = _myTransform.position.z - _target.position.z;


    }

    private void FixedUpdate()
    {
        if (isWater)
        {
            _myTransform.position = new Vector3(0, _myTransform.position.y, _target.position.z + _offsetZ);
        }
        else
        {
            _myTransform.position = new Vector3(0, 0, _target.position.z + _offsetZ);
        }
        
    }
}
