using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateblePlatformComponent : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(new Vector3(1, 0, 0), 45 * Time.deltaTime);
    }
}
