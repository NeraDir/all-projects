using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatfromPlayerTrigger : MonoBehaviour
{
    public delegate void DetectPlayer(bool indexState);
    public static event DetectPlayer PlayerDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovementManager panther))
        {
            if (PlayerDetected != null)
                PlayerDetected(true);
        }
    }
}
