using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planescript : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.position += new Vector3(0, direction.y,0) * 5 * Time.deltaTime;
    }
}
