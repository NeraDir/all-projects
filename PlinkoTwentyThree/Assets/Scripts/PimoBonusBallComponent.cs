using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimoBonusBallComponent : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 4);
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(transform.forward * 250, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PimoBonusCellComponent cell))
        {
            cell.OnTrigger();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        PimoGameController._ballsCount -= 1;
    }
}
