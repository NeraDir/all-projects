using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTriggerZone : Obstacle
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out TigerEntityColliderManager tiger))
        {
            Debug.Log("TIGER");
        }
    }
}
