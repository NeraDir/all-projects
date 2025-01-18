using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundcom : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position += new Vector3(-1, 0, 0) * 2 * Time.deltaTime;
    }
}
