using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BULLETCOMPONENT : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 40, ForceMode.Impulse);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MOVEMENTCOMPONENT movement))
        {
            if (movement.transform == target)
            {
                movement.speedMultiplayer -= 0.5f;
            }
            Destroy(gameObject);
        }
    }
}
