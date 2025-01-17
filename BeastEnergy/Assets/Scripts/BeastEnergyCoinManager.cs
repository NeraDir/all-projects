using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastEnergyCoinManager : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 180 * Time.deltaTime);
    }
}
