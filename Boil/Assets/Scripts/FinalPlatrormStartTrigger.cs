using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlatrormStartTrigger : MonoBehaviour
{

    private FinalPlatrorm parent; 

    public void SetParrent(FinalPlatrorm parent)
    {
        this.parent = parent;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Ball ball))
        {
            parent.CallBallOnStartFinalPlatformEvent();
        }
    }
}
