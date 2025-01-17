using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderObjectMoveToPlane : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position += new Vector3(0, 0, 1) * GameManager.moveSpeed * Time.deltaTime;
    }
}
