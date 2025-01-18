using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    void Start()
    {

    }
    void Update()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, -10);
    }
}
