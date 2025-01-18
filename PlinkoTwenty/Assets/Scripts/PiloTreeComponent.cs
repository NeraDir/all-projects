using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiloTreeComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PiloRoadComponent road))
        {
            Destroy(gameObject);
        }
    }
}
