using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviPlanePlayerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IAviTriggerComponent trigger))
        {
            if (!AviGameComponent.AviGameIsPlay)
                return;
            trigger.Use();
        }
    }
}
