using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineSlottriggerer : MonoBehaviour
{
    private int index;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out MachineSlotItem slotTime))
        {
            index = slotTime.itemIndex;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        index = 1000;
    }

    public int GetIndex()
    {
        return index;
    }
}
