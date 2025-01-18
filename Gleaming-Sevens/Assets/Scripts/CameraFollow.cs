using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Vector3 offcet;

    [SerializeField]
    private Transform target;

    private Vector3 followPos;
    private Transform myTranform;

    [SerializeField]
    private float speed;

    private void OnEnable()
    {
        myTranform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        followPos = new Vector3(target.position.x + offcet.x, target.position.y + offcet.y, target.position.z + offcet.z);
        transform.position = Vector3.Lerp(myTranform.position, followPos, speed);
    }
    /*
    private void LateUpdate()    
    {
        followPos = new Vector3(target.position.x + offcet.x, target.position.y + offcet.y, target.position.z + offcet.z);
        transform.position = Vector3.Lerp(myTranform.position, followPos, speed);
    }
    */
}
