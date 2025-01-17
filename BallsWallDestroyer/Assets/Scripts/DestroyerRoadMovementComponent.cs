using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerRoadMovementComponent : MonoBehaviour
{
    private void Start()
    {
        Destroy(transform.parent.gameObject, 8);
    }
}
