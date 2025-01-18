using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfCamController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BallController ball))
        {
            CamController.stopMove?.Invoke();
            
        }
    }
}
