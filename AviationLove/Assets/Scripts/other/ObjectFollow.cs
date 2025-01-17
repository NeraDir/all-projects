using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offest;

    public Vector3 directions;

    public float speed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x * directions.x,target.position.y * directions.y,target.position.z * directions.z) + offest, speed * Time.deltaTime);
    }
}
