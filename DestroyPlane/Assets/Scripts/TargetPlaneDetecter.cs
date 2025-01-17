using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlaneDetecter : MonoBehaviour
{
    public delegate void DetectTargetPlaneDelegate();
    public static DetectTargetPlaneDelegate TargetPlaneDetectedEvent;

    private Collider2D lastCollider;

    private void OnEnable()
    {
        lastCollider = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Plane plane))
        {

            if (lastCollider != collision)
            {
                if (Game.targetPlaneColor == plane.planeColor)
                {
                    if (TargetPlaneDetectedEvent != null)
                    {
                        TargetPlaneDetectedEvent();
                    }

                    lastCollider = collision;
                }

              

            }


        }
    }
}
