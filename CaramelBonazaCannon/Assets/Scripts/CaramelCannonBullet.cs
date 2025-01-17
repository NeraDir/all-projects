using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaramelCannonBullet : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 50, ForceMode.Impulse);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CaramelCannonEnemieComponent enemie))
        {
            enemie.Death(CaramelCanonGameManager.CaramelCannonBulletDamage);
            Destroy(gameObject);
        }
    }
}
