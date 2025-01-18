using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PimoRoadMover : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, 1) * PimoTargetMove.moveSpeed * Time.deltaTime;
    }
}
