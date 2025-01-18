using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldComponente : MonoBehaviour
{
    private void Start()
    {
        if (Random.Range(0,6) != 0)
        {
            Destroy(gameObject);
        }
    }
}
