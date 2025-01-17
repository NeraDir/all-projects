using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 3f;

    void FixedUpdate()
    {
        transform.position += new Vector3(0, _speed*Time.deltaTime, 0);
    }
}
