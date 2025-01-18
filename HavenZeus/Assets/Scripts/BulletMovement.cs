using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed;
   

    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if(ShopManager._currentBowIndex == 3)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.forward * _movementSpeed * Time.deltaTime);
    }
}
