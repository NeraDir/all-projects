using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlatformController : MonoBehaviour
{
    private Rigidbody goldPlatformBody;

    private void Start()
    {
        goldPlatformBody = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        Vector3 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direct = new Vector3(0, -direction.y, 0);

        goldPlatformBody.velocity = direct * 5f;
    }
}
