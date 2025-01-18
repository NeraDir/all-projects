using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SawComponent : MonoBehaviour
{


    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 360 * Time.deltaTime);
    }
}
