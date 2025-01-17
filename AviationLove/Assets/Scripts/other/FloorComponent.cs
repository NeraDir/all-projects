using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorComponent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterJUmp character))
        {
            FindObjectOfType<FloorsSpawnManager>().SpawnFloor();
            Destroy(gameObject.transform.parent.gameObject, 20);
        }
    }
}
