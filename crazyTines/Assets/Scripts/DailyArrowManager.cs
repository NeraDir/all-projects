using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyArrowManager : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DailyPlacemanager place))
        {
            DailyAdditionalManager.winValue = place.winValue;
        }
    }
}
