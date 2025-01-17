using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLootAtCam : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(new Vector3( Camera.main.transform.position.x, Camera.main.transform.position.y + 180 , Camera.main.transform.position.z) );
    }
}
