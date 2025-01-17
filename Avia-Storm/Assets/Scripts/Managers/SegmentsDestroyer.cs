using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentsDestroyer : MonoBehaviour
{
    public MapGenerator Generator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BuffForCollide>() != null)
        {
            Destroy(other.transform.parent.gameObject);
            Generator.SpawnSegment();
        }
    }
}
