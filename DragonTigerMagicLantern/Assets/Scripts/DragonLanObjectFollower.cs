using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DragonLanObjectFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 directions;

    public static bool move;

    private void LateUpdate()
    {
        if (move)
            return;
        transform.position = Vector3.Lerp(transform.position, new Vector3(directions.x * (target.position.x + offset.x), directions.y * (target.position.y + offset.y), directions.z * (target.position.z + offset.z)), speed * Time.deltaTime);        
    }
}
