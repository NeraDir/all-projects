using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkArrowComponente : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out StalkSliderPlaces stalkPlace))
        {
            StalkGamingManager.placeState = stalkPlace.PlaceState;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        StalkGamingManager.placeState = "Break";
    }
}
