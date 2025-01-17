using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengerObjectFollower : MonoBehaviour
{
    public Vector3 directionFollower;

    public Transform followerTarget;

    public float followerSpeed;

    private void LateUpdate()
    {
        transform.position += directionFollower * followerSpeed * Time.deltaTime;
    }
}
