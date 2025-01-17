using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentMovement : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.right * 0.5f * Time.deltaTime;
    }
}
