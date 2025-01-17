using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTimer : MonoBehaviour
{
    public float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
