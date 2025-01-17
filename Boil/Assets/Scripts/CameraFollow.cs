using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public float lerpValue;

    private Vector3 cameraPosition;




    private void FixedUpdate()
    {
        cameraPosition = new Vector3(followTarget.transform.position.x + offset.x, followTarget.transform.position.y + offset.y, followTarget.transform.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, lerpValue);
    }

    /*
    private void LateUpdate()
    {
        cameraPosition = new Vector3(followTarget.transform.position.x + offset.x, followTarget.transform.position.y + offset.y, followTarget.transform.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, lerpValue);

    }
    */

}
