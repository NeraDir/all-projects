using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelcomponents : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(new Vector3(1, 0, 0),90 * Time.deltaTime);
    }
}
