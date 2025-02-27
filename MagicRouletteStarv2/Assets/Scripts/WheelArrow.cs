using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelArrow : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WheelItemComponent item))
        {
            PreGameComponent.targetSprite = item.type;
            Debug.Log(PreGameComponent.targetSprite);
        }
    }
}
