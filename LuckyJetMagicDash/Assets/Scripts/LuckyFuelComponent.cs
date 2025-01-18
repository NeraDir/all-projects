using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyFuelComponent : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 1, 0), 180 * Time.deltaTime);
    }
}
