using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(0, 1* 90 * Time.deltaTime, 0);
    }
}
