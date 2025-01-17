using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengerRotorRotation : MonoBehaviour
{
    public float godogdfigd = 1;

    public Vector3 direction = new Vector3(0, 0, 1);

    private void LateUpdate()
    {
        transform.Rotate(direction, (360 * godogdfigd) * Time.deltaTime);
    }
}
