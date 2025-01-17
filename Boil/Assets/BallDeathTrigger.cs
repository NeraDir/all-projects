using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeathTrigger : MonoBehaviour
{

    public delegate void BallTrigerDelegate();
    public static event BallTrigerDelegate BallTrigerEvent;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Ball ball))
        {
            if (BallTrigerEvent != null)
            {
                BallTrigerEvent();
            }

        }
    }
}
