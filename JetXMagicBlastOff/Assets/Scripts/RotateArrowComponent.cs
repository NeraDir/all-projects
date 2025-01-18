using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrowComponent : MonoBehaviour
{
    public bool lineActive;

    public bool ArrowOnLine()
    {
        return lineActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RotateNeedPosComponent isOnLine))
        {
            lineActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        lineActive = false;
    }
}
