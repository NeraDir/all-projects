using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlinkosComponent plinko))
        {
            MiniGameController.UseEvent((int)plinko.xValue);
        }
    }
}
