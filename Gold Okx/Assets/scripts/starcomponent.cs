using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starcomponent : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 360 * Time.deltaTime);
    }
}
