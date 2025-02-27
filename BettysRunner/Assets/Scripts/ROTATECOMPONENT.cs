using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTATECOMPONENT : MonoBehaviour
{
     private float _speed = 15;
    [SerializeField] private Vector3 _direction;

    private void LateUpdate()
    {
        transform.Rotate(_direction, _speed * Time.deltaTime);
    }
}
