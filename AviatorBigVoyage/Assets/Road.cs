using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    void FixedUpdate()
    {
        if(transform.position.z < -5.3)
        {
            transform.position = new Vector3(0, 0, 10.5f);
        }
    }
}
