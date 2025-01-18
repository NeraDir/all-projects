using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _distanceFromObject;
    public GameObject _object;
    [SerializeField] private float speed = 5f;

    void Update()
    {
        if (_object != null)
        {
            Vector3 positionToGo = _object.transform.position + _distanceFromObject;

            transform.position = Vector3.Lerp(transform.position, positionToGo, Time.deltaTime * 150f);
        }
        //transform.LookAt(_object.transform.position);
    }
}
