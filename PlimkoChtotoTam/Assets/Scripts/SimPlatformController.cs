using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimPlatformController : MonoBehaviour
{
    private Rigidbody2D platform;

    private void Start()
    {
        platform = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseDrag()
    {
        platform.bodyType = RigidbodyType2D.Dynamic;

        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        platform.velocity = direction * 5;

    }

    private void OnMouseUp()
    {
        platform.bodyType = RigidbodyType2D.Kinematic;
        platform.velocity = Vector3.zero;
    }
}
