using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetObjectMovement : MonoBehaviour
{
    public float moveSpeed;

    private void LateUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
    }
}
