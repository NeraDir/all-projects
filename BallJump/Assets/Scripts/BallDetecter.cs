using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BallDetecter : MonoBehaviour
{
    public delegate void BallTriggerChecker();
    public static event BallTriggerChecker ExitBallLastSegmentWasFixed;

    private bool hasTrigger;


    private void OnEnable()
    {
        hasTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            if (!hasTrigger)
            {
                hasTrigger = true;

                if (ExitBallLastSegmentWasFixed != null)
                {
                    ExitBallLastSegmentWasFixed();
                }

                Destroy(transform.parent.gameObject);
            }
        }


    }
}
