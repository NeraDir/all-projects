using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBold : MonoBehaviour
{
    Rigidbody lightBoldBody;

    private void Start()
    {
        lightBoldBody = GetComponent<Rigidbody>();
        lightBoldBody.AddForce(transform.up * 20, ForceMode.Impulse);
        Destroy(lightBoldBody.gameObject,4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth health))
        {
            health.TakeDamage();
            Destroy(gameObject);
        }
    }
}
