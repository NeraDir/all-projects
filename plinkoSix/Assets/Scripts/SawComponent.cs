using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawComponent : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 1, 0), 180 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Balls ball))
        {
            ball.gameManager.activeBalls.Remove(ball);
            Destroy(ball.gameObject);
        }
    }
}
