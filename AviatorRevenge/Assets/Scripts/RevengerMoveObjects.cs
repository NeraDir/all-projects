using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengerMoveObjects : MonoBehaviour
{
    public Vector3 direction;

    private void LateUpdate()
    {
        transform.position += direction * 5 * Time.deltaTime;
    }
}
