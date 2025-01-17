using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTriggerPlaceComponent : MonoBehaviour
{
    public ChaseSlotComponent item;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ChaseSlotComponent item))
        {
            this.item = item;
        }
    }
}
