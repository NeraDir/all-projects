using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody rb;

    private bool dieByShoot;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 5, ForceMode.Impulse);
        Destroy(gameObject, 10);
        StartCoroutine(SetMaxSize());
    }

    private IEnumerator SetMaxSize() 
    {
        while (transform.localScale != new Vector3(1, 1, 1)) 
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), 5 * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemieManager enemie))
        {
            enemie.TakeDamage(1);
            dieByShoot = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (dieByShoot)
        {
            CannonController.combos++;
        }
        else
        {
            CannonController.combos = 0;
        }
    }
}
