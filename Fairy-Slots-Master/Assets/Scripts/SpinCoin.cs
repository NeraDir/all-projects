using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCoin : MonoBehaviour
{
    private bool oneShotTrigger;

    private void OnEnable()
    {
        oneShotTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent(out Boat boat))
        {
            if (!oneShotTrigger)
            {
                oneShotTrigger = true;
                MainGameManager.currenttSpinCount++;
                Destroy(gameObject);
            }
        }

        
    }
}
