using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclesTrigger : MonoBehaviour
{
    public int currentIndex;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CirclesComponent circler))
        {
            currentIndex = circler.GetXValue();
        }
    }
}
